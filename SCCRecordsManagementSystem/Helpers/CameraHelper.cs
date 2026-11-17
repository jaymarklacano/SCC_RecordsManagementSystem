using OpenCvSharp;
using System;
using System.Drawing;

namespace SCCRecordsManagementSystem
{
    // Opens the webcam and reads pictures (frames) from it.
    public class CameraHelper : IDisposable
    {
        // How many reads in a row may fail before we decide the webcam was disconnected.
        private const int MaxFailedReads = 10;

        private VideoCapture capture;
        private int failedReads;

        public bool IsRunning
        {
            get { return capture != null; }
        }

        // True after too many failed reads in a row (for example the USB cable was removed).
        public bool IsDisconnected
        {
            get { return failedReads >= MaxFailedReads; }
        }

        // Starts the webcam. Returns "" when it worked, otherwise a message to show to the user.
        public string Start()
        {
            Stop();

            try
            {
                // Try the first 3 cameras (0 = built-in webcam, 1 and 2 = USB webcams).
                for (int cameraIndex = 0; cameraIndex < 3; cameraIndex++)
                {
                    VideoCapture candidate = new VideoCapture(cameraIndex, VideoCaptureAPIs.DSHOW);

                    if (candidate.IsOpened())
                    {
                        candidate.Set(VideoCaptureProperties.FrameWidth, 640);
                        candidate.Set(VideoCaptureProperties.FrameHeight, 480);

                        if (WaitForFirstFrame(candidate))
                        {
                            capture = candidate;
                            failedReads = 0;
                            return "";
                        }
                    }

                    candidate.Dispose();
                }
            }
            catch (DllNotFoundException)
            {
                return "The camera library could not be loaded.\n\n" +
                       "Please install the \"Microsoft Visual C++ Redistributable (x64)\" and make sure the " +
                       "\"dll\" folder is next to the program (see README.md).";
            }
            catch (BadImageFormatException)
            {
                return "The camera library does not match this computer (32-bit / 64-bit).\n\nPlease see the troubleshooting section of README.md.";
            }
            catch (Exception)
            {
                return "The webcam could not be started.";
            }

            return "No webcam was found or it cannot be opened.\n\n" +
                   "Check that the webcam is plugged in, that no other program (Zoom, Teams, Camera app) is using it, " +
                   "and that camera access is allowed in Windows Settings > Privacy > Camera.";
        }

        // Reads one picture from the webcam. The picture is mirrored (like a mirror), which is easier to use.
        // Returns null when no picture could be read.
        public Bitmap ReadFrame()
        {
            if (capture == null)
            {
                return null;
            }

            try
            {
                using (Mat frame = new Mat())
                {
                    if (!capture.Read(frame) || frame.Empty())
                    {
                        failedReads++;
                        return null;
                    }

                    failedReads = 0;
                    Cv2.Flip(frame, frame, FlipMode.Y);
                    return ImageHelper.MatToBitmap(frame);
                }
            }
            catch (Exception)
            {
                failedReads++;
                return null;
            }
        }

        public void Stop()
        {
            if (capture != null)
            {
                capture.Release();
                capture.Dispose();
                capture = null;
            }
        }

        public void Dispose()
        {
            Stop();
        }

        // Some webcams need a moment before the first real picture arrives.
        private static bool WaitForFirstFrame(VideoCapture candidate)
        {
            using (Mat frame = new Mat())
            {
                for (int attempt = 0; attempt < 30; attempt++)
                {
                    if (candidate.Read(frame) && !frame.Empty())
                    {
                        return true;
                    }
                    System.Threading.Thread.Sleep(50);
                }
            }
            return false;
        }
    }
}
