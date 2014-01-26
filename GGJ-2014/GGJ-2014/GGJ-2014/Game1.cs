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
using GGJ_2014.Creatures;

namespace GGJ_2014
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int PLAYER_SPAWN_X = 100;
        public const int PLAYER_SPAWN_Y = 100;

        public const int POPUP_DISPLAY_POSITION_X = 10;
        public const int POPUP_DISPLAY_POSITION_Y = 10;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyState;
        KeyboardState prevKeyState;
        MouseState mouseState;
        MouseState prevMouseState;

        SpriteFont myFont;

        Player player;


        MenuBorderedTextItem eggCounter;
        MenuBorderedTextItem playerStats;
        string playerEggCount;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            Camera.ScreenSize = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Camera.Focus(0, 0);

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
               new MenuScreen(MenuScreenType.QUESTIONS_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.GAMEPLAY, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.PAUSED, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.PAUSE_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.CREDITS_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.WIN_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.LOSE_MENU, Color.CornflowerBlue));


            keyState = Keyboard.GetState();
            prevKeyState = keyState;
            mouseState = Mouse.GetState();
            prevMouseState = mouseState;

            //NEEDS TO CHANGE 

            //TextureStorage.GetInstance().LoadContent(Content);

            #region Load_Generated_Textures

            TextureStorage.GetInstance().AddTexture(Textures.NONE, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.NONE, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_DIRT, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_DIRT, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_GRASS, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_GRASS, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_COBBLESTONE, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_COBBLESTONE, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_PAVEMENT, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_PAVEMENT, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_TREE_ON_GRASS, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_TREE_ON_GRASS, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_PINETREE_ON_GRASS, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_PINETREE_ON_GRASS, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_PINETREE_STUMP, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_PINETREE_STUMP, null));
            TextureStorage.GetInstance().AddTexture(Textures.CREATURE_CHICKEN, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.CREATURE_CHICKEN, null));
            TextureStorage.GetInstance().AddTexture(Textures.CHICKEN_EGG, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.CHICKEN_EGG, null));
            TextureStorage.GetInstance().AddTexture(Textures.CREATURE_GENERIC, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.CREATURE_GENERIC, null, 12, 12));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_BRICK_WALL, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_BRICK_WALL, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_WOOD_PLANK, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_WOOD_PLANK, null));

            #endregion

            player = new Player(TextureStorage.GetInstance().GetTexture(Textures.CREATURE_GENERIC), new Vector2(PLAYER_SPAWN_X, PLAYER_SPAWN_Y));


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

            MenuSystem.GetInstance().LoadContent(myFont, GraphicsDevice);

            #region GameplayLoadContent

            Level.GetInstance().AddCreature(player);

            MenuSystemNS.MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.GAMEPLAY)
                .AddControl(
                new MenuSystemNS.MenuButton(
                    Vector2.Zero,
                    "Regen Map!",
                    Color.White,
                    () => { ResetGame(); }
                    ));

            eggCounter = new MenuBorderedTextItem(
                   new Vector2(
                       graphics.PreferredBackBufferWidth * 0.01f,
                       graphics.PreferredBackBufferHeight * 0.9f
                       ),
                       Color.PeachPuff,
                       string.Format("{0:000000} x Eggs Collected", Player.Eggs)
                       );
            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(eggCounter);


            playerStats = new MenuBorderedTextItem(
                   new Vector2(
                       graphics.PreferredBackBufferWidth * 0.75f,
                       graphics.PreferredBackBufferHeight * 0.01f
                       ),
                       Color.PeachPuff,
                       string.Format("Compassion: {0:F2}\nLuck: {1:F2}\nStrength: {2:F2}", Player.Compassion, Player.Luck, Player.Strength)
                       );
            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(playerStats);

            #endregion

            #region QuestionaireLoadContent

            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.QUESTIONS_MENU)
                .AddControl(
                new MultipleChoiceQuiz()
                );

            #endregion

            #region MainMenuLoadContent

            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.MAIN_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(50, 250),
                   "Let's Play!",
                   Color.White,
                   () => { MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.QUESTIONS_MENU); }
                   ));

            #endregion





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

        private void ResetGame()
        {
            Level.GetInstance().LoadLevel(); 
            player = new Player(TextureStorage.GetInstance().GetTexture(Textures.CREATURE_GENERIC), new Vector2(PLAYER_SPAWN_X, PLAYER_SPAWN_Y)); 
            Level.GetInstance().AddCreature(player);
            Player.Eggs = 0;
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
                        Level.GetInstance().Update(gameTime);
                        player.HandleInput(keyState);
                        Camera.Focus(player.MiddlePosition);
                        eggCounter.Text = string.Format("{0:000000} x Eggs Collected", Player.Eggs);
                        playerStats.Text = string.Format("Compassion: {0:F2}\nLuck: {1:F2}\nStrength: {2:F2}", Player.Compassion, Player.Luck, Player.Strength);

                        break;
                    }

                case MenuScreenType.PAUSED:
                    {

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

            /*
            Console.WriteLine("intelligence: " + Player.Intelligence);
            Console.WriteLine("charisma: " + Player.Charisma);
            Console.WriteLine("strength: " + Player.Strength);
            //*/
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            MenuSystem.GetInstance().DrawUnderlay();


            switch (MenuSystem.GetInstance().CurrentScreenType)
            {
                case MenuScreenType.MAIN_MENU:
                    {

                        break;
                    }

                case MenuScreenType.GAMEPLAY:
                    {

                        // TODO: Add your drawing code here
                        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);

                        //Add Game Draw code Here
                        Level.GetInstance().Draw(spriteBatch);

                        spriteBatch.End();

                        break;
                    }

                case MenuScreenType.PAUSED:
                    {

                        // TODO: Add your drawing code here
                        spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, Camera.CameraMatrix);

                        //Add Game Draw code Here
                        Level.GetInstance().Draw(spriteBatch);

                        spriteBatch.End();

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


            spriteBatch.Begin();

            MenuSystem.GetInstance().DrawOverlay(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
