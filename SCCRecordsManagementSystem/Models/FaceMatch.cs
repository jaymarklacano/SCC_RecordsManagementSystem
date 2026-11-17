namespace SCCRecordsManagementSystem
{
    // The answer of FaceService.Recognize().
    public class FaceMatch
    {
        // True when the face is close enough to a registered student.
        public bool IsMatch { get; set; }

        // The matched student (only valid when IsMatch is true).
        public int StudentId { get; set; }

        // How different the faces are. LOWER = more similar (0 = identical).
        public double Distance { get; set; }
    }
}
