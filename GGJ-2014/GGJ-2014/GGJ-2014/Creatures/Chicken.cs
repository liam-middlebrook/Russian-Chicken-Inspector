using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.Creatures
{
    class Chicken : Creature
    {

        public Chicken(Vector2 position)
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_CHICKEN), position, "chicken")
        {
            walkSpeed = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            Random rand = new Random();

            double randVal = rand.NextDouble();
            switch (directionFacing)
            {
                case Direction.NORTH:
                    {
                        if (randVal > .975)
                        {
                            directionFacing = Direction.EAST;
                        }
                        else if (randVal > .95)
                        {
                            directionFacing = Direction.WEST;
                        }
                        else if (randVal > .9)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        break;
                    }
                case Direction.EAST:
                    {
                        if (randVal > .975)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        else if (randVal > .95)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        else if (randVal > .9)
                        {
                            directionFacing = Direction.WEST;
                        }
                        break;
                    }
                case Direction.SOUTH:
                    {
                        if (randVal > .975)
                        {
                            directionFacing = Direction.EAST;
                        }
                        else if (randVal > .95)
                        {
                            directionFacing = Direction.WEST;
                        }
                        else if (randVal > .9)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        break;
                    }
                case Direction.WEST:
                    {
                        if (randVal > .975)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        else if (randVal > .95)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        else if (randVal > .9)
                        {
                            directionFacing = Direction.EAST;
                        }
                        break;
                    }
            }
            Walk(directionFacing);
            base.Update(gameTime);
        }

        public override void HandleInput(KeyboardState keyState)
        {

        }

        public override void Interact(Creature user)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
