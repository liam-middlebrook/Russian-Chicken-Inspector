using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GGJ_2014.Graphics;
using GGJ_2014.MenuSystemNS;

namespace GGJ_2014.Creatures
{
    class GoldenEgg
        : Egg, Interactable
    {
        public GoldenEgg(Vector2 position)
            : base(position)
        {
            Scale = new Vector2(0.5f, .75f);
            TintColor = Color.Yellow;
            canPickUp = false;
        }

        public override Rectangle GetCollisionBox()
        {
            return new Rectangle((int)Position.X-Texture.Width/4, (int)Position.Y-Texture.Height/8,(int)Texture.Width, (int)Texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public void Interact(Creature user)
        {
            MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.PAUSED);

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 10),
                    "[1] Steal the Golden Egg.",
                    Color.White,
                    () =>
                    {
                        removeEgg = true;
                        Player.Eggs += (int)Math.Abs(Player.GOLDEN_EGG_VALUE*1.5);
                        Player.Compassion -= (int)(MathHelper.Clamp(10 * -Player.Compassion,-20, 20));
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.Tomato, string.Format("You stole {0} eggs and lost {1} compassion", Player.GOLDEN_EGG_VALUE, (int)(MathHelper.Clamp(10 * -Player.Compassion,-20, 20))), 5.0f));
                    },
                    Microsoft.Xna.Framework.Input.Keys.D1
            ));

            MenuSystem
                .GetInstance()
                .GetMenuScreenOfType(MenuScreenType.PAUSED)
                .AddControl(
                new MenuButton(
                    new Vector2(10, 60),
                    "[2] Admire the Golden Egg.",
                    Color.White,
                    () =>
                    {
                        Random random = new Random();
                        MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                        MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.PAUSED).menuControls.Clear();
                        if (random.NextDouble() * 280 < Player.Luck + Player.Compassion * 2)
                        {
                            removeEgg = true;
                            Player.Eggs += Player.GOLDEN_EGG_VALUE;
                            MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.Gold, string.Format("You were donated {0} eggs!", Player.GOLDEN_EGG_VALUE), 5.0f));
                        }
                        else
                        {
                            MenuSystem.GetInstance().CurrentScreen.AddControl(new MenuBorderedTextItem(new Vector2(Game1.POPUP_DISPLAY_POSITION_X, Game1.POPUP_DISPLAY_POSITION_Y), Color.PeachPuff, "Nothing happened. Try Raising your compassion.", 3.0f));
                        }
                    },
                    Microsoft.Xna.Framework.Input.Keys.D2
            ));
        }

        public bool IsAlive()
        {
            return !RemoveEgg;
        }

        public override int EggsGiven
        {
            get
            {
                return Player.GOLDEN_EGG_VALUE;
            }
        }

        public string GetIdentifier()
        {
            return "Golden Egg";
        }
    }
}
