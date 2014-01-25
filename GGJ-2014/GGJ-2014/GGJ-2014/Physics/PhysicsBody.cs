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

        private Vector2 velocity;

        public PhysicsBody(Texture2D texture, Vector2 position)
            : base(texture, position)
        {

        }

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
