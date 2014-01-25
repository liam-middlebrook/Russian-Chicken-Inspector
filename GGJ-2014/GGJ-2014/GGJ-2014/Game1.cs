using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GGJ_2014.Graphics;
using GGJ_2014.MenuSystemNS;
using GGJ_2014.Levels;

namespace GGJ_2014
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyState;
        KeyboardState prevKeyState;
        MouseState mouseState;
        MouseState prevMouseState;

        SpriteFont myFont;

        Texture2D testTexture;

        Level level = new Level();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            Camera.ScreenSize = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            MenuSystem.GetInstance()
               .AddMenuScreen(
               new MenuScreen(MenuScreenType.MAIN_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.GAMEPLAY, Color.Red));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.PAUSE_MENU, Color.ForestGreen));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.CREDITS_MENU, Color.DarkOrchid));


            keyState = Keyboard.GetState();
            prevKeyState = keyState;
            mouseState = Mouse.GetState();
            prevMouseState = mouseState;



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            myFont = Content.Load<SpriteFont>("SpriteFont1");

            MenuSystem.GetInstance().Initalize(myFont, GraphicsDevice);

            testTexture = TextureGenerator.GenerateTexture(GraphicsDevice, Textures.DIRT);
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            switch (MenuSystem.GetInstance().CurrentScreenType)
            {
                case MenuScreenType.MAIN_MENU:
                    {

                        break;
                    }

                case MenuScreenType.GAMEPLAY:
                    {
                        level.Update(gameTime);
                        break;
                    }

                case MenuScreenType.PAUSE_MENU:
                    {

                        break;
                    }

                case MenuScreenType.CREDITS_MENU:
                    {

                        break;
                    }
            }

            MenuSystem.GetInstance().Update(keyState, prevKeyState, mouseState, prevMouseState, gameTime);

            prevKeyState = keyState;
            keyState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MenuSystem.GetInstance().DrawUnderlay();


            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);

            //Add Game Draw code Here
            level.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            MenuSystem.GetInstance().DrawOverlay(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
