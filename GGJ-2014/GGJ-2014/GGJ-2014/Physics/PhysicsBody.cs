using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014.Physics
{
    public class PhysicsBody
        : Sprite
    {
        public const float DRAG = 0.9f;
        public const float ZERO_VELOCITY_THRUSHHOLD = 0.001f;

        private Vector2 velocity;

        public PhysicsBody(Texture2D texture, Vector2 position)
            : base(texture, position)
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (Math.Abs(velocity.X) < ZERO_VELOCITY_THRUSHHOLD)
            {
                velocity.X = 0;
            }
            if (Math.Abs(velocity.Y) < ZERO_VELOCITY_THRUSHHOLD)
            {
                velocity.Y = 0;
            }
            Position += velocity;
            velocity *= DRAG;
        }

        public void ApplyVelocity(float x, float y)
        {
            ApplyVelocityX(x);
            ApplyVelocityY(y);
        }

        public void ApplyVelocityX(float x)
        {
            velocity.X += x;
        }

        public void ApplyVelocityY(float y)
        {
            velocity.Y += y;
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
