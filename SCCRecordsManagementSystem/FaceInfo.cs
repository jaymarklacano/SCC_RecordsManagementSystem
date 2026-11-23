using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCRecordsManagementSystem
{
    // One face that the face detector (YuNet) found in a camera picture.
    public class FaceInfo
    {
        // The box around the face (used for the colored box and the quality checks).
        public Rect Box { get; set; }

        // 5 points on the face, always in this order:
        // [0] right eye, [1] left eye, [2] nose tip, [3] right mouth corner, [4] left mouth corner.
        // They are used to turn and resize the face so the eyes are always at the same place.
        public Point2f[] Landmarks { get; set; }

        // How sure the detector is that this is a real face (0.0 to 1.0).
        public float Score { get; set; }
    }
}
