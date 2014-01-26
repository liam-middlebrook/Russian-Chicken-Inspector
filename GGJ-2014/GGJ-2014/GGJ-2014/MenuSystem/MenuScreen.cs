using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    enum MenuScreenType
    {
        MAIN_MENU,
        GAMEPLAY,
        PAUSED,
        PAUSE_MENU,
        QUESTIONS_MENU,
        CREDITS_MENU,
        WIN_MENU,
        LOSE_MENU
    }

    class MenuScreen
    {
        protected MenuScreenType menuScreenType;

        protected Color backgroundColor;

        public MenuScreenType MenuScreenType { get { return menuScreenType; } }

        public List<MenuControl> menuControls;


        public MenuScreen(MenuScreenType menuScreenType, Color backgroundColor)
        {
            menuControls = new List<MenuControl>();

            this.menuScreenType = menuScreenType;

            this.backgroundColor = backgroundColor;
        }

        public void AddControl(MenuControl control)
        {
            menuControls.Add(control);
        }

        public void RemoveControl(MenuControl control)
        {
            menuControls.Remove(control);
        }

        public virtual void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            for (int i = 0; i < menuControls.Count; i++)
            {
                menuControls[i].Update(keyState, prevKeyState, mouseState, prevMouseState);
            }
        }

        public virtual void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch)
        {
            foreach (MenuControl control in menuControls)
            {
                control.Draw(spriteBatch);
            }
        }

        public virtual void Clear(GraphicsDevice graphics)
        {
            graphics.Clear(backgroundColor);
        }

    }
}
