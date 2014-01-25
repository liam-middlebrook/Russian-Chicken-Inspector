using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GGJ_2014
{
    enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    public abstract class Creature
        : PhysicsBody
    {
        private string identifier = "FILL THIS OUT";
        private Direction directionFacing = Direction.NORTH;
        private float walkSpeed = 3.0f;

        public Creature(Texture2D texture, Vector2 position, string identifier)
            : base(texture, position)
        {
            this.identifier = identifier;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Walk(bool north, bool east, bool south, bool west)
        {
            if (north)
            {
                directionFacing = Direction.NORTH;
            }
            else if(east)
            {
                directionFacing = Direction.EAST;
            }
            else if(south)
            {
                directionFacing = Direction.SOUTH;
            }
            else if(west)
            {
                directionFacing = Direction.WEST;
            }
            Vector2 velocity = Velocity;
            velocity.X += east ? walkSpeed : 0;
            velocity.X -= west ? walkSpeed : 0;
            velocity.Y += south ? walkSpeed : 0;
            velocity.Y -= north ? walkSpeed : 0;
        }

        public abstract void Interact(Creature user);

        public string Identifyer
        {
            get
            {
                return identifier;
            }
        }
    }
}
