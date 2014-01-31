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
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_CHICKEN), position, "Chicken")
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
                Levels.Level.GetInstance().AddEgg(new Egg(MiddlePosition));
                timePassed -= EGG_SPAWN_MILISECONDS;
            }
            base.Update(gameTime);
        }

        public override void HandleInput(KeyboardState keyState)
        {

        }

        public override void Interact(Creature user)
        {
            MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.PAUSED);

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 10),
                    "[1] Kill the Chicken.",
                    Color.White,
                    () =>
                    {
                        Random random = new Random();
                        int eggsGained = (int)Math.Round(2.0f * Player.Luck + rand.NextDouble());
                        float compassonLost = 0.2f * 1.0f / Player.Luck;
                        isAlive = false;
                        Player.Strength += 0.01f + 0.01f * Player.Luck;
                        Player.Compassion -= compassonLost;
                        Player.Eggs += eggsGained;
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        MenuSystem.GetInstance()
                            .CurrentScreen.AddControl(
                            new MenuBorderedTextItem(
                                new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y),
                                Color.Tomato,
                                string.Format("You Gained {0} Egg and lost {1:F2} Compassion", eggsGained, compassonLost),
                                5.0f
                                ));
                    },
                    Keys.D1,
                    Microsoft.Xna.Framework.Input.Keys.NumPad1
            ));

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 60),
                    "[2] Rescue the Chicken.",
                    Color.White,
                    () =>
                    {
                        float compassonGoten = 0.2f * 1.0f / Player.Luck;
                        isAlive = false;
                        ++Player.Eggs;
                        Player.Compassion += compassonGoten;
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        MenuSystem.GetInstance()
                            .CurrentScreen
                            .AddControl(
                            new MenuBorderedTextItem(
                                new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y),
                                Color.SpringGreen,
                                string.Format("You Gained 1 Egg and {0:F2} Compassion", compassonGoten),
                                5.0f));
                    },
                    Keys.D2,
                    Microsoft.Xna.Framework.Input.Keys.NumPad2
            ));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
