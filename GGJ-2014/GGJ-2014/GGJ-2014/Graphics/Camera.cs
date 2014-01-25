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
        private static Rectangle screenSize = new Rectangle(0, 0, 800, 600);
        private static Rectangle viewBounds = new Rectangle(0, 0, 0, 0);

        public static void Focus(Vector2 focalPoint)
        {
            Focus(focalPoint.X, focalPoint.Y);
        }

        public static void Focus(float x, float y)
        {
            float cameraX =- x + screenSize.Width / 2.0f;
            float cameraY = -y + screenSize.Height / 2.0f;
            cameraMatrix = Matrix.CreateTranslation(cameraX, cameraY, 1.0f);
            viewBounds = new Rectangle((int)Math.Round(cameraX), (int)Math.Round(cameraY), screenSize.Width, screenSize.Height);
        }

        public static Matrix CameraMatrix
        {
            get
            {
                return cameraMatrix;
            }
        }

        public static Rectangle ScreenSize
        {
            set
            {
                screenSize = value;
            }
        }

        public static Rectangle ViewBounds
        {
            get
            {
                return viewBounds;
            }
        }
    }
}
