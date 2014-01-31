using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GGJ_2014.MenuSystemNS;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.Creatures
{
    class Villager
        : Person
    {
        public static Color[] villagerColors = {Color.Khaki, Color.LightGreen, Color.Peru, Color.PaleVioletRed, Color.SkyBlue, Color.SeaGreen};

        public const int VILLAGER_DESPAWN_TIME = 600000;
        public const int VILLAGER_EGGS_TO_GIVE_THRESHOLD = 250;
        private Random random = new Random();
        private float idleCounter = 0;
        private int timeElapsed = 0;
        private int eggsToGive;

        public Villager(Vector2 position)
            : base(Graphics.TextureStorage.GetInstance().GetTexture(Graphics.Textures.CREATURE_VILLAGER), position)
        {
            Random rand = new Random();
            TintColor = villagerColors[rand.Next(0, villagerColors.Length)];
            walkSpeed = 0.1f;

            eggsToGive = (int)(rand.NextDouble() * VILLAGER_EGGS_TO_GIVE_THRESHOLD);
        }

        public override void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
            if (random.NextDouble() < 0.03f)
            {
                directionFacing = GetRandomDirection();
            }
            if (idleCounter < 0)
            {
                Walk(directionFacing);
                idleCounter += (float)random.NextDouble();
            }
            else if(random.NextDouble() < 0.05f)
            {
                idleCounter -= 5;
            }
            if (timeElapsed >= VILLAGER_DESPAWN_TIME)
            {
                isAlive = false;
            }
        }

        protected override void CollidedWithTile(Levels.Tile t)
        {
            base.CollidedWithTile(t);
            Direction oldDirection = directionFacing;
            while (oldDirection == directionFacing)
            {
                directionFacing = GetRandomDirection();
            }
        }

        private Direction GetRandomDirection()
        {
            return GetDirectionFromInt(random.Next(0, 4));
        }

        private Direction GetDirectionFromInt(int dir)
        {
            switch (dir)
            {
                case 0:
                    return Direction.EAST;
                case 1:
                    return Direction.NORTH;
                case 2:
                    return Direction.SOUTH;
                case 3:
                    return Direction.WEST;
            }
            return Direction.INVALID;
        }

        public override void Interact(Creature user)
        {
            Random random = new Random();
            MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.PAUSED);

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 10),
                    "[1] Rob the person.",
                    Color.White,
                    () =>
                    {
                        eggsToGive = (int)(eggsToGive * 1.5 * Player.Luck * Math.Max(Player.Strength, 1));
                        int damage = (int)Math.Max(random.Next(1, 30) - Player.Strength/10, 0);
                        float compassonLost = 2.4f * 1.0f / Player.Luck;
                        isAlive = false;
                        Player.Strength += (float)(0.02f * random.NextDouble()+ 0.02f * Player.Luck);
                        Player.Compassion -= compassonLost;
                        Player.Eggs += eggsToGive*2;
                        Player.Health -= damage;
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.Tomato, string.Format("You Gained {0} Egg and lost {1} Health", eggsToGive*2, damage), 5.0f));
                    },
                   new List<Microsoft.Xna.Framework.Input.Keys>(new []{Microsoft.Xna.Framework.Input.Keys.D1, Microsoft.Xna.Framework.Input.Keys.NumPad1})
            ));

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 60),
                    "[2] Ask for Eggs.",
                    Color.White,
                    () =>
                    {
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        if (eggsToGive > 0)
                        {
                            int eggsGiven = random.Next(0, (int)(Math.Max(Player.Compassion,0.0f) * eggsToGive + Player.Luck * 4));
                            eggsToGive -= eggsGiven;
                            Player.Eggs += eggsGiven;
                            if (eggsGiven > 0)
                            {
                                MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.SpringGreen, string.Format("You were donated {0} eggs!", eggsGiven), 5.0f));
                            }
                            else
                            {
                                MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.PeachPuff, "Didn't get anything.", 5.0f));
                            }
                        }
                        else
                        {
                            MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.PeachPuff, "Out of eggs to give.", 5.0f));
                        }
                    },
                   new List<Keys>(new []{Keys.D2, Keys.NumPad2})
            ));
        }
    }
}
