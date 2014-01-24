using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GGJ_2014.Graphics
{
    class Camera
    {
        private static Matrix cameraMatrix;
        private static Rectangle screenSize = new Rectangle(0, 0, 800, 500);

        public const float SCALE = 2.0f;
        

        public static void Focus(float x, float y)
        {
            cameraMatrix = Matrix.CreateTranslation(-x + screenSize.Width / 2, -y + screenSize.Height / 2, 1.0f);
        }

        public static Matrix CameraMatrix
        {
            get
            {
                return cameraMatrix;
            }
        }
    }
}
