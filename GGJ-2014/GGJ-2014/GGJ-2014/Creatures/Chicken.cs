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
        public const int EGG_SPAWN_MAX_RAND = 30000;
        public const int EGG_SPAWN_MILISECONDS = 20000;

        private int timePassed = 0;

        public Chicken(Vector2 position)
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_CHICKEN), position, "chicken")
        {
            walkSpeed = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            timePassed += gameTime.ElapsedGameTime.Milliseconds;

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
            if (timePassed >= EGG_SPAWN_MILISECONDS + rand.Next(0, EGG_SPAWN_MAX_RAND))
            {
                Levels.Level.GetInstance().EggList.Add(new Egg(MiddlePosition));
                timePassed -= EGG_SPAWN_MILISECONDS;
            }
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
