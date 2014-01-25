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
            collisionBox.X = (int)Position.X - collisionBox.Width/2;
            collisionBox.Y = (int)Position.Y - collisionBox.Height/2;
        }

        public void CheckLevelCollisions()
        {
            int minX = Level.GetInstance().GetTileIndexInBoundsX(collisionBox.X / Tile.TILE_SIZE);
            int maxX = Level.GetInstance().GetTileIndexInBoundsX((collisionBox.X + collisionBox.Width) / Tile.TILE_SIZE + 1);
            int minY = Level.GetInstance().GetTileIndexInBoundsY(collisionBox.Y / Tile.TILE_SIZE);
            int maxY = Level.GetInstance().GetTileIndexInBoundsY((collisionBox.Y +collisionBox.Height) / Tile.TILE_SIZE + 1);
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    Tile tile = Level.GetInstance().GetTile(x, y);
                    SyncCollitionBox();
                    if (tile != null && tile.IsSolid)
                    {
                        Rectangle tileRectangle = tile.TileRectangle;
                        Vector2 collitionResolve = new Vector2(0, 0);
                        bool tileNorth = IsCollidableTileAdjacent(x, y, 0, -1);
                        bool tileEast = IsCollidableTileAdjacent(x, y, 1, 0);
                        bool tileSouth = IsCollidableTileAdjacent(x, y, 0, 1);
                        bool tileWest = IsCollidableTileAdjacent(x, y, -1, 0);

                        Rectangle intersection = Rectangle.Intersect(collisionBox, tileRectangle);
                        Console.WriteLine(intersection);

                        Position -= new Vector2(intersection.Width, intersection.Height);

                        if (collisionBox.Bottom > tileRectangle.Top && !tileNorth) // Down
                        {
                            collitionResolve.Y = collisionBox.Bottom - tileRectangle.Top;
                        }
                        if (collisionBox.Top < tileRectangle.Bottom && !tileSouth) //Top
                        {
                            collitionResolve.Y = collisionBox.Top - tileRectangle.Bottom;
                        }
                        SyncCollitionBox();
                        //if (collisionBox.Left > tileRectangle.Right && !tileWest) //Right
                        //{
                        //    collitionResolve.X = tileRectangle.Left - collisionBox.Right;
                        //}
                        //else if (collisionBox.Left < tileRectangle.Right && !tileEast) //Left
                        //{
                        //    collitionResolve.X = collisionBox.Left - tileRectangle.Right;
                        //}

                        //Position -= collitionResolve;
                    }
                }
            }
        }

        private bool IsCollidableTileAdjacent(int originX, int originY, int directionX, int directionY)
        {
            Tile tile = Level.GetInstance().GetTile(originX + directionX, originY + directionY);
            return tile == null || (tile != null && tile.IsSolid);
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
