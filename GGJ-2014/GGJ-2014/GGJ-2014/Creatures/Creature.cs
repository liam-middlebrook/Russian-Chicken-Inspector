﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GGJ_2014.Levels;

namespace GGJ_2014.Creatures
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
        protected Direction directionFacing = Direction.NORTH;
        protected float walkSpeed = 0.5f;
        protected Rectangle collisionBox;

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
                        Rectangle intersection = Rectangle.Intersect(collisionBox, tileRectangle);
                        //if (intersection.Width > 0)
                        //{
                        //    Console.WriteLine("X");
                        //    Position -= new Vector2(Velocity.X / PhysicsBody.DRAG + 0.5f * Math.Sign(Velocity.X), 0);
                        //}
                        //if (intersection.Height > 0)
                        //{
                        //    Console.WriteLine("Y");
                        //    Position -= new Vector2(0, Velocity.Y / PhysicsBody.DRAG + 0.5f * Math.Sign(Velocity.Y));
                        //}

                        bool tileNorth = IsCollidableTileAdjacent(x, y, 0, -1);
                        bool tileEast = IsCollidableTileAdjacent(x, y, 1, 0);
                        bool tileSouth = IsCollidableTileAdjacent(x, y, 0, 1);
                        bool tileWest = IsCollidableTileAdjacent(x, y, -1, 0);

                        int downDepth = Math.Abs(collisionBox.Bottom - tileRectangle.Top);
                        int upDepth = Math.Abs(collisionBox.Top - tileRectangle.Bottom);
                        int leftDepth = Math.Abs(collisionBox.Right - tileRectangle.Left);
                        int rightDepth = Math.Abs(collisionBox.Left - tileRectangle.Right);

                        if (collisionBox.Bottom > tileRectangle.Top && downDepth < upDepth && !tileNorth && Velocity.Y > 0) // Down
                        {
                            Position -= new Vector2(0, intersection.Height);
                        }
                        if (collisionBox.Top < tileRectangle.Bottom && upDepth < downDepth && !tileSouth && Velocity.Y < 0) //Top
                        {
                            Position += new Vector2(0, intersection.Height);
                        }
                        SyncCollitionBox();
                        if (collisionBox.Right > tileRectangle.Left && leftDepth < rightDepth && !tileWest && Velocity.X > 0) //Left
                        {
                            Position -= new Vector2(intersection.Width, 0);
                        }
                        if (collisionBox.Left < tileRectangle.Right && rightDepth < leftDepth && !tileEast && Velocity.X < 0) //Right
                        {
                            Position += new Vector2(intersection.Width, 0);
                        }
                    }
                }
            }
        }

        private bool IsCollidableTileAdjacent(int originX, int originY, int directionX, int directionY)
        {
            Tile tile = Level.GetInstance().GetTile(originX + directionX, originY + directionY);
            return tile == null || (tile != null && tile.IsSolid);
        }

        public abstract void HandleInput(KeyboardState keyState);

        protected void Walk(Direction walkDirection)
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

        protected Tile GetTileInFrontOf()
        {
            return Level.GetInstance().GetTile((int)(MiddlePosition.X / Tile.TILE_SIZE + Math.Round(Math.Cos(Rotation - Math.PI / 2))), (int)(MiddlePosition.Y / Tile.TILE_SIZE + Math.Round(Math.Sin(Rotation - Math.PI / 2))));
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

        public Rectangle CollitionBox
        {
            get
            {
                return collisionBox;
            }
        }
    }
}
