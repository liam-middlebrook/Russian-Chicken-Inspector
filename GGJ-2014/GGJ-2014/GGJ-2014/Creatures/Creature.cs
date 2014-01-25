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
        private float walkSpeed = 0.3f;

        public Creature(Texture2D texture, Vector2 position, string identifier)
            : base(texture, position)
        {
            this.RotationOrigin = new Vector2(texture.Width/2, texture.Height/2);
            this.identifier = identifier;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CheckLevelCollitions();
        }

        public void CheckLevelCollitions()
        {
            //for(int x = Level.GetInstan
        }

        public void Walk(bool north, bool east, bool south, bool west)
        {
            if (north)
            {
                directionFacing = Direction.NORTH;
                Rotation = 0;
            }
            else if(east)
            {
                directionFacing = Direction.EAST;
                Rotation = (float)Math.PI/2;
            }
            else if(south)
            {
                directionFacing = Direction.SOUTH;
                Rotation = (float)Math.PI;
            }
            else if(west)
            {
                directionFacing = Direction.WEST;
                Rotation = (float)(3*Math.PI/2);
            }
            ApplyVelocityX(east ? walkSpeed : 0);
            ApplyVelocityX(west ? -walkSpeed : 0);
            ApplyVelocityY(south ? walkSpeed : 0);
            ApplyVelocityY(north ? -walkSpeed : 0);
        }

        public abstract void Interact(Creature user);

        public string Identifyer
        {
            get
            {
                return identifier;
            }
        }

        public Vector2 MiddlePosition
        {
            get
            {
                return new Vector2(Position.X , Position.Y);
            }
        }
    }
}
