﻿using System;
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
            viewBounds = new Rectangle((int)Math.Round(-x + screenSize.Width / 2.0), (int)Math.Round(-y + screenSize.Height / 2.0), screenSize.Width, screenSize.Height);
            cameraMatrix = Matrix.CreateTranslation(viewBounds.X, viewBounds.Y, 1.0f);
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
