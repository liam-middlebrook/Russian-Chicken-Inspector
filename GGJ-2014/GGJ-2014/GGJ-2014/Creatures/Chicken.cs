using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GGJ_2014.Physics;
using GGJ_2014.MenuSystemNS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.Creatures
{
    class Chicken : Creature
    {
        public const int EGG_SPAWN_MAX_RAND = 30000;
        public const int EGG_SPAWN_MILISECONDS = 20000;

        private Random rand = new Random();
        private int timePassed = 0;

        public Chicken(Vector2 position)
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_CHICKEN), position, "chicken")
        {
            walkSpeed = 0.25f;
        }

        public override void Update(GameTime gameTime)
        {
            timePassed += gameTime.ElapsedGameTime.Milliseconds;


            double randVal = rand.NextDouble();
            switch (directionFacing)
            {
                case Direction.NORTH:
                    {
                        if (randVal > .99)
                        {
                            directionFacing = Direction.EAST;
                        }
                        else if (randVal > .98)
                        {
                            directionFacing = Direction.WEST;
                        }
                        else if (randVal > .965)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        break;
                    }
                case Direction.EAST:
                    {
                        if (randVal > .99)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        else if (randVal > .98)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        else if (randVal > .965)
                        {
                            directionFacing = Direction.WEST;
                        }
                        break;
                    }
                case Direction.SOUTH:
                    {
                        if (randVal > .99)
                        {
                            directionFacing = Direction.EAST;
                        }
                        else if (randVal > .98)
                        {
                            directionFacing = Direction.WEST;
                        }
                        else if (randVal > .965)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        break;
                    }
                case Direction.WEST:
                    {
                        if (randVal > .99)
                        {
                            directionFacing = Direction.NORTH;
                        }
                        else if (randVal > .98)
                        {
                            directionFacing = Direction.SOUTH;
                        }
                        else if (randVal > .965)
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
            Console.WriteLine("I hate you" + DateTime.Now.Millisecond);
            MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.PAUSED);

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 10),
                    "Kill the Chicken.",
                    Color.White,
                    () =>
                    {
                        Levels.Level.GetInstance().CreatureList.Remove(this);
                        Player.Strength += 0.01f + 0.01f * Player.Luck;
                        Player.Compassion -= 0.2f * 1.0f / Player.Luck;
                        Player.Eggs += (int)(2.0f * Player.Luck);
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                    }
            ));

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 60),
                    "Rescue the Chicken.",
                    Color.White,
                    () =>
                    {
                        Levels.Level.GetInstance().CreatureList.Remove(this);
                        ++Player.Eggs;
                        Player.Compassion += 0.2f * 1.0f / Player.Luck;
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                    }
            ));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
