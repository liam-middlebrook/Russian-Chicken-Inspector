using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Graphics;
using Microsoft.Xna.Framework;

namespace GGJ_2014.Physics
{
    class PhysicsBody
        : Sprite
    {
        public const float DRAG = 0.9f;

        private Vector2 velocity;


        public virtual void Update(GameTime gameTime)
        {
            Position += velocity;
            velocity *= DRAG;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }
    }
}
