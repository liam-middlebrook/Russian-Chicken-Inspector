using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;

namespace GGJ_2014
{
    enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }
    class Creature
        : PhysicsBody
    {
        private string identifier;
        private Direction directionFacing;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Velocity.X > 0)
            {
                directionFacing = Direction.EAST;
            }
            else if(Velocity.X < 0)
            {
                directionFacing = Direction.WEST;
            }
            if (Velocity.Y > 0)
            {
                directionFacing = Direction.SOUTH;
            }
            else if (Velocity.Y < 0)
            {
                directionFacing = Direction.NORTH;
            }
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
