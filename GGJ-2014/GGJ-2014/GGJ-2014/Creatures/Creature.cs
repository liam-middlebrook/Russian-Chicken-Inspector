using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Levels;

namespace GGJ_2014
{
    public enum Direction
    {
        INVALID,
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
        private Rectangle collisionBox;

        public Creature(Texture2D texture, Vector2 position, string identifier)
            : base(texture, position)
        {
            this.RotationOrigin = new Vector2(texture.Width/2, texture.Height/2);
            this.identifier = identifier;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SyncCollitionBox();
            CheckLevelCollisions();
        }

        protected void SyncCollitionBox()
        {
            collisionBox.X = (int)Position.X;
            collisionBox.Y = (int)Position.Y;
        }

        public void CheckLevelCollisions()
        {

        }

        public void HandleInput(KeyboardState keyState)
        {
            Direction direction = 0;
            direction = keyState.IsKeyDown(Keys.W) ? Direction.NORTH : direction;
            direction = keyState.IsKeyDown(Keys.S) ? Direction.SOUTH : direction;
            direction = keyState.IsKeyDown(Keys.A) ? Direction.WEST : direction;
            direction = keyState.IsKeyDown(Keys.D) ? Direction.EAST : direction;
            Walk(direction);
        }

        private void Walk(Direction walkDirection)
        {
            directionFacing = walkDirection;
            switch (walkDirection)
            {
                case Direction.NORTH:
                    Rotation = 0;
                    ApplyVelocityY(-walkSpeed);
                    break;
                case Direction.EAST:
                    Rotation = (float)Math.PI/2;
                    ApplyVelocityX(walkSpeed);
                    break;
                case Direction.SOUTH:
                    Rotation = (float)Math.PI;
                    ApplyVelocityY(walkSpeed);
                    break;
                case Direction.WEST:
                    Rotation = (float)(3 * Math.PI / 2);
                    ApplyVelocityX(-walkSpeed);
                    break;
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

        public Vector2 MiddlePosition
        {
            get
            {
                return new Vector2(Position.X , Position.Y);
            }
        }
    }
}
