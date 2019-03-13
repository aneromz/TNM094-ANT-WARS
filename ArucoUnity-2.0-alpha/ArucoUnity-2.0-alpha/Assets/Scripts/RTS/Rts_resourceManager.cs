using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS { 

    public class Rts_resourceManager : MonoBehaviour {

        public static float ScrollSpeed { get { return 25.0f; } }
        public static float RotateSpeed { get { return 100.0f; } }

        public static int ScrollWidth { get { return 15; } }

        public static float MinCameraHeight { get { return 10.0f; } }
        public static float MaxCameraHeight { get { return 40.0f; } }
    }

}