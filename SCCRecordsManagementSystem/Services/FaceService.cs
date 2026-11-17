using OpenCvSharp;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace SCCRecordsManagementSystem
{
    // Face detection and face recognition using OpenCV (OpenCvSharp).
    //
    // How it works (simple version):
    //  1. DETECT: a Haar cascade finds the position of faces in a camera picture.
    //  2. PREPARE: the face is cut out, turned to gray, resized to 100x100 and its contrast is balanced.
    //  3. TRAIN: the LBPH recognizer learns all registered face pictures (5 per student).
    //  4. RECOGNIZE: a new face is compared with all learned pictures. The closest one wins,
    //     but only if the "distance" is small enough. Otherwise the face is "unknown".
    public class FaceService : IDisposable
    {
        // A face smaller than this (in pixels) is too far away to be recognized well.
        public const int MinFaceSize = 90;

        // Faces smaller than this are ignored completely (people far in the background).
        private const int MinDetectSize = 60;

        // A face must be at least this fraction (60%) of the biggest face to count as "another person".
        private const double MinRelativeFaceSize = 0.6;

        // A face is accepted as a match when the distance is below this number.
        // Lower = stricter (fewer wrong matches), higher = easier to recognize but riskier.
        // Change this value if recognition is too strict or too easy.
        public const double MatchThreshold = 70.0;

        // Pictures with a lower sharpness value than this are called "blurry".
        private const double MinSharpness = 15.0;

        // Size of the face picture used by the recognizer and of the pictures saved in the database.
        private const int RecognizerSize = 100;
        private const int SavedImageSize = 160;

        private CascadeClassifier faceDetector;
        private LBPHFaceRecognizer recognizer;
        private bool isTrained;

        // Filled by LoadRegisteredFaces()
        public int TrainedStudentCount { get; private set; }
        public int TrainedImageCount { get; private set; }

        public bool IsTrained
        {
            get { return isTrained; }
        }

        public FaceService()
        {
            string cascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "haarcascade_frontalface_default.xml");

            if (!File.Exists(cascadePath))
            {
                throw new FileNotFoundException(
                    "The face detection file was not found:\n" + cascadePath +
                    "\n\nMake sure the \"Data\" folder is next to the program.");
            }

            faceDetector = new CascadeClassifier(cascadePath);
            if (faceDetector.Empty())
            {
                throw new InvalidDataException("The face detection file is damaged:\n" + cascadePath);
            }

            recognizer = LBPHFaceRecognizer.Create();
        }

        // ---------- 1. Detect ----------

        // Returns the rectangles of all faces found in the picture.
        public Rectangle[] DetectFaces(Bitmap picture)
        {
            using (Mat color = ImageHelper.BitmapToMat(picture))
            using (Mat gray = new Mat())
            {
                Cv2.CvtColor(color, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.EqualizeHist(gray, gray);

                Rect[] found = faceDetector.DetectMultiScale(
                    gray, 1.1, 5, HaarDetectionTypes.ScaleImage,
                    new OpenCvSharp.Size(MinDetectSize, MinDetectSize));

                // The detector sometimes finds small "faces" in the background or on clothes.
                // A real second person next to the student has a face of a similar size, so
                // faces much smaller than the biggest face are ignored.
                int biggestWidth = 0;
                foreach (Rect rect in found)
                {
                    biggestWidth = Math.Max(biggestWidth, rect.Width);
                }

                List<Rectangle> faces = new List<Rectangle>();
                foreach (Rect rect in found)
                {
                    if (rect.Width >= biggestWidth * MinRelativeFaceSize)
                    {
                        faces.Add(new Rectangle(rect.X, rect.Y, rect.Width, rect.Height));
                    }
                }
                return faces.ToArray();
            }
        }

        // Checks that a detected face is good enough. Returns "" if it is good,
        // otherwise a short message that tells the user what to fix.
        public string CheckFaceQuality(Bitmap picture, Rectangle face)
        {
            if (face.Width < MinFaceSize)
            {
                return "Your face is too small. Please move closer to the camera.";
            }

            if (face.X < 3 || face.Y < 3 || face.Right > picture.Width - 3 || face.Bottom > picture.Height - 3)
            {
                return "Your face is too close to the edge. Please move to the center of the camera.";
            }

            using (Mat color = ImageHelper.BitmapToMat(picture))
            using (Mat faceColor = new Mat(color, ToRect(face)))
            using (Mat gray = new Mat())
            using (Mat small = new Mat())
            using (Mat edges = new Mat())
            {
                Cv2.CvtColor(faceColor, gray, ColorConversionCodes.BGR2GRAY);

                double brightness = Cv2.Mean(gray).Val0;
                if (brightness < 60)
                {
                    return "The picture is too dark. Please turn on more light.";
                }
                if (brightness > 200)
                {
                    return "The picture is too bright. Please avoid strong light behind or on your face.";
                }

                // Sharpness = how much detail the face has. A blurry face has very little.
                Cv2.Resize(gray, small, new OpenCvSharp.Size(RecognizerSize, RecognizerSize));
                Cv2.Laplacian(small, edges, MatType.CV_64F);
                Scalar mean, standardDeviation;
                Cv2.MeanStdDev(edges, out mean, out standardDeviation);
                double sharpness = standardDeviation.Val0 * standardDeviation.Val0;
                if (sharpness < MinSharpness)
                {
                    return "The picture is blurry. Please hold still and look at the camera.";
                }
            }

            return "";
        }

        // Cuts the face out of the picture and returns it as JPEG bytes (this is saved in the database).
        public byte[] CropFaceToJpeg(Bitmap picture, Rectangle face)
        {
            using (Bitmap crop = new Bitmap(SavedImageSize, SavedImageSize))
            using (Graphics graphics = Graphics.FromImage(crop))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(picture, new Rectangle(0, 0, SavedImageSize, SavedImageSize), face, GraphicsUnit.Pixel);
                return ImageHelper.ToJpegBytes(crop);
            }
        }

        // ---------- 2. Train ----------

        // Loads all saved face pictures from the database and teaches them to the recognizer.
        // Call this again after students were added, changed or deleted.
        public void LoadRegisteredFaces()
        {
            DataTable table = StudentService.GetAllFaceImages();

            List<Mat> faces = new List<Mat>();
            List<int> labels = new List<int>();
            HashSet<int> students = new HashSet<int>();

            try
            {
                foreach (DataRow row in table.Rows)
                {
                    int studentId = Convert.ToInt32(row["student_id"]);

                    using (Mat saved = Cv2.ImDecode((byte[])row["image_data"], ImreadModes.Color))
                    {
                        if (saved.Empty())
                        {
                            continue;   // skip a damaged picture
                        }

                        faces.Add(PrepareFace(saved));
                        labels.Add(studentId);
                        students.Add(studentId);
                    }
                }

                if (faces.Count > 0)
                {
                    // The label of each picture is the student_id, so the answer tells us who it is.
                    LBPHFaceRecognizer newRecognizer = LBPHFaceRecognizer.Create();
                    newRecognizer.Train(faces, labels);

                    recognizer.Dispose();
                    recognizer = newRecognizer;
                }

                isTrained = faces.Count > 0;
                TrainedImageCount = faces.Count;
                TrainedStudentCount = students.Count;
            }
            finally
            {
                foreach (Mat face in faces)
                {
                    face.Dispose();
                }
            }
        }

        // ---------- 3. Recognize ----------

        // Finds out which registered student the face belongs to.
        public FaceMatch Recognize(Bitmap picture, Rectangle face)
        {
            FaceMatch result = new FaceMatch();

            if (!isTrained)
            {
                return result;
            }

            using (Mat color = ImageHelper.BitmapToMat(picture))
            using (Mat faceColor = new Mat(color, ToRect(face)))
            using (Mat prepared = PrepareFace(faceColor))
            {
                int label;
                double distance;
                recognizer.Predict(prepared, out label, out distance);

                result.StudentId = label;
                result.Distance = distance;
                result.IsMatch = label > 0 && distance <= MatchThreshold;
            }

            return result;
        }

        // ---------- Helpers ----------

        // Gray + 100x100 + balanced contrast. Used for BOTH the saved pictures and the live camera.
        private static Mat PrepareFace(Mat colorFace)
        {
            Mat gray = new Mat();
            Cv2.CvtColor(colorFace, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Resize(gray, gray, new OpenCvSharp.Size(RecognizerSize, RecognizerSize));
            Cv2.EqualizeHist(gray, gray);
            return gray;
        }

        private static Rect ToRect(Rectangle rectangle)
        {
            return new Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public void Dispose()
        {
            if (recognizer != null)
            {
                recognizer.Dispose();
                recognizer = null;
            }
            if (faceDetector != null)
            {
                faceDetector.Dispose();
                faceDetector = null;
            }
        }
    }
}
