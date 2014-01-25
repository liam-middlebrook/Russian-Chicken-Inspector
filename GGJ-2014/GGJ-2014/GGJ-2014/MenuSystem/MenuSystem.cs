using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GGJ_2014.MenuSystemNS
{
    class MenuSystem
    {
        #region SINGLETON_ATTRIBUTES AND METHODS

        static MenuSystem instance;

        public static MenuSystem GetInstance()
        {
            if (instance == null)
            {
                instance = new MenuSystem();
            }
            return instance;
        }

        #endregion

        private Dictionary<MenuScreenType, MenuScreen> menuScreens;

        private MenuScreen currentScreen;

        private SpriteFont menuFont;

        private GraphicsDevice graphicsDevice;

        public SpriteFont MenuFont { get { return menuFont; } }

        public GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }

        public MenuScreen CurrentScreen { get { return currentScreen; } }

        public MenuScreenType CurrentScreenType { get { return currentScreen.MenuScreenType; } }

        private MenuSystem()
        {
            menuScreens = new Dictionary<MenuScreenType, MenuScreen>();
        }

        public void Initalize(SpriteFont font, GraphicsDevice graphicsDevice)
        {
            this.menuFont = font;
            this.graphicsDevice = graphicsDevice;
        }
        public void AddMenuScreen(MenuScreen menuScreenToAdd)
        {
            menuScreens.Add(menuScreenToAdd.MenuScreenType, menuScreenToAdd);
            if (menuScreens.Count == 1)
            {
                currentScreen = menuScreens[0];
            }
        }

        public MenuScreen GetMenuScreenOfType(MenuScreenType menuScreenType)
        {
            if (menuScreens.ContainsKey(menuScreenType))
            {
                return menuScreens[menuScreenType];
            }

            return null;
        }

        public void SwitchToMenuScreenOfType(MenuScreenType menuScreenType)
        {
            if (menuScreens.ContainsKey(menuScreenType))
            {
                currentScreen = menuScreens[menuScreenType];
            }
        }

        public void Update(KeyboardState keyState, KeyboardState prevKeyState, MouseState mouseState, MouseState prevMouseState, GameTime gameTime)
        {
            currentScreen.Update(keyState, prevKeyState, mouseState, prevMouseState, gameTime);
        }

        public void DrawUnderlay()
        {
            if (currentScreen != null)
            {
                currentScreen.Clear(GraphicsDevice);
            }
            else
            {
                graphicsDevice.Clear(Color.White);
            }
        }

        public void DrawOverlay(SpriteBatch spriteBatch)
        {
            if (currentScreen != null)
            {
                currentScreen.Draw(GraphicsDevice, spriteBatch);
            }
        }
    }
}
