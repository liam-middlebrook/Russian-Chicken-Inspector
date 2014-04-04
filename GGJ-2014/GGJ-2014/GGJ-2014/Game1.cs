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
        EggFallWindow eggFall;

        MenuBorderedTextItem eggCounter;
        MenuBorderedTextItem eggTimer;
        MenuBorderedTextItem playerStats;
        MenuBorderedTextItem healthBar;

        int timeElaspsed;

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
               new MenuScreen(MenuScreenType.MAIN_MENU, Color.Black));
            MenuSystem.GetInstance()
               .AddMenuScreen(
               new MenuScreen(MenuScreenType.QUESTIONS_MENU, Color.Black));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.GAMEPLAY, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.PAUSED, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.PAUSE_MENU, Color.Black));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.CREDITS_MENU, Color.CornflowerBlue));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.WIN_MENU, Color.Black));
            MenuSystem.GetInstance()
                .AddMenuScreen(
                new MenuScreen(MenuScreenType.LOSE_MENU, Color.Black));


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
            TextureStorage.GetInstance().AddTexture(Textures.CREATURE_VILLAGER, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.CREATURE_VILLAGER, null, 14, 14));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_BRICK_WALL, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_BRICK_WALL, null));
            TextureStorage.GetInstance().AddTexture(Textures.TILE_WOOD_PLANK, TextureGenerator.GenerateTexture(GraphicsDevice, Textures.TILE_WOOD_PLANK, null));

            #endregion

            //player = new Player(TextureStorage.GetInstance().GetTexture(Textures.CREATURE_GENERIC), new Vector2(PLAYER_SPAWN_X, PLAYER_SPAWN_Y));
            eggFall = new EggFallWindow();

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

            //MenuSystemNS.MenuSystem.GetInstance()
            //    .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.GAMEPLAY)
            //    .AddControl(
            //    new MenuSystemNS.MenuButton(
            //        Vector2.Zero,
            //        "Regen Map!",
            //        Color.White,
            //        () => { ResetGame(); }
            //        ));

            eggCounter = new MenuBorderedTextItem(
                   new Vector2(
                       graphics.PreferredBackBufferWidth * 0.01f,
                       graphics.PreferredBackBufferHeight * 0.9f
                       ),
                       Color.PeachPuff,
                       string.Empty
                       );
            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(eggCounter);

            eggTimer = new MenuBorderedTextItem(
                    new Vector2(
                        graphics.PreferredBackBufferWidth * .65f,
                        graphics.PreferredBackBufferHeight * .9f
                        ),
                        Color.PeachPuff,
                        string.Empty
                        );
            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(eggTimer);

            playerStats = new MenuBorderedTextItem(
                   new Vector2(
                       graphics.PreferredBackBufferWidth * 0.72f,
                       graphics.PreferredBackBufferHeight * 0.01f
                       ),
                       Color.PeachPuff,
                       string.Empty
                       );
            MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(playerStats);

            MenuSystemNS.MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuScreenType.GAMEPLAY)
                .AddControl(
                new MenuHiddenButton(
                   new List<Keys>(new[] { Keys.Escape }),
                    () => { MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.PAUSE_MENU); }
            ));
            healthBar = new MenuBorderedTextItem(
                new Vector2(
                    graphics.PreferredBackBufferWidth * 0.8f,
                    graphics.PreferredBackBufferHeight * 0.8f
                    ),
                    Color.PeachPuff,
                    "Health: 100"
                    );
            MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.GAMEPLAY).AddControl(healthBar);

            #endregion

            #region MainMenuLoadContent

            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.MAIN_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 500),
                   "[Space] Let's Play!",
                   Color.White,
                   () =>
                   {
                       MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.QUESTIONS_MENU);
                       ResetGame();
                       MenuSystem.GetInstance().GetMenuScreenOfType(MenuScreenType.QUESTIONS_MENU).AddControl(new MultipleChoiceQuiz());
                   },
                   new List<Keys>(
                       new[]{
                           Keys.Space
                       })
                   ));
            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.MAIN_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 550),
                   "[Escape] Exit Game",
                   Color.White,
                   () =>
                   {
                       Exit();
                   },
                   new List<Keys>(
                       new[]{
                           Keys.Escape
                       })
                   ));

            MenuSystemNS.MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.MAIN_MENU)
                .AddControl(
                new MenuBorderedTextItem(Vector2.Zero, Color.White, "Russian Chicken Inspector"));


            #endregion

            #region WIN_MENU

            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.WIN_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 500),
                   "[Enter] Return to Main Menu",
                   Color.White,
                   () => { MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.MAIN_MENU); },
                   new List<Keys>(
                       new[] {
                           Keys.Enter
                       })
                   ));

            MenuSystemNS.MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.WIN_MENU)
                .AddControl(
                new MenuBorderedTextItem(Vector2.Zero, Color.White, "You Win!"));

            #endregion

            #region LOSE_MENU

            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.LOSE_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 500),
                   "[Enter] Return to Main Menu",
                   Color.White,
                   () => { MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.MAIN_MENU); },
                   new List<Keys>(
                       new[] {
                           Keys.Enter
                       })
                   ));

            MenuSystemNS.MenuSystem.GetInstance()
                .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.LOSE_MENU)
                .AddControl(
                new MenuBorderedTextItem(Vector2.Zero, Color.White, "You Lose."));

            #endregion

            #region PAUSE_MENU


            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.PAUSE_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 500),
                   "[Space] Continue Playing!",
                   Color.White,
                   () =>
                   {
                       MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.GAMEPLAY);
                   },
                   new List<Keys>(
                       new[] {
                           Keys.Space
                       })
                   ));

            MenuSystemNS.MenuSystem.GetInstance()
               .GetMenuScreenOfType(MenuSystemNS.MenuScreenType.PAUSE_MENU)
               .AddControl(
               new MenuSystemNS.MenuButton(
                   new Vector2(10, 450),
                   "[Esc] Return to the Main Menu (Resets Game)!",
                   Color.White,
                   () =>
                   {
                       MenuSystem.GetInstance().SwitchToMenuScreenOfType(MenuScreenType.MAIN_MENU);
                   },
                   new List<Keys>(
                       new[] {
                           Keys.Escape
                       })
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
            timeElaspsed = 0;
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
            if (eggFall != null)
            {
                eggFall.Update(gameTime);
            }
            switch (MenuSystem.GetInstance().CurrentScreenType)
            {
                case MenuScreenType.MAIN_MENU:
                    {
                        eggFall.isActive = true;
                        break;
                    }

                case MenuScreenType.GAMEPLAY:
                    {
                        timeElaspsed += gameTime.ElapsedGameTime.Milliseconds;
                        eggFall.isActive = false;
                        Level.GetInstance().Update(gameTime);
                        player.HandleInput(keyState);
                        Camera.Focus(player.MiddlePosition);
                        eggCounter.Text = string.Format("{0:000000} x Eggs Collected", Player.Eggs);
                        eggTimer.Text = string.Format("{0:000.0} Seconds Remaining", Player.TimeRemaining);
                        playerStats.Text = string.Format("Compassion: {0:F2}\nLuck: {1:F2}\nStrength: {2:F2}", Player.Compassion, Player.Luck, Player.Strength);
                        healthBar.Text = string.Format("Health: {0}%", Player.Health);

                        break;
                    }

                case MenuScreenType.PAUSED:
                    {
                        eggFall.isActive = false;
                        break;
                    }

                case MenuScreenType.PAUSE_MENU:
                    {
                        eggFall.isActive = true;
                        break;
                    }

                case MenuScreenType.CREDITS_MENU:
                    {
                        eggFall.isActive = true;
                        break;
                    }
                default:
                    eggFall.isActive = true;
                    break;
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

            if (eggFall != null)
            {
                spriteBatch.Begin();
                eggFall.Draw(spriteBatch);
                spriteBatch.End();
            }
            switch (MenuSystem.GetInstance().CurrentScreenType)
            {
                case MenuScreenType.MAIN_MENU:
                    {
                        spriteBatch.Begin();
                        spriteBatch.DrawString(myFont, "[WASD] or Arrow Keys to move.\n"
                                                        + "[Space Bar] to interact with things and chop down trees.\n\n"
                                                        + "Collect eggs by doing various tasks.\n"
                                                        + "Collect more eggs by completing secret acheivments!\n"
                                                         + "\tInteract with Villagers!\n"
                                                         + "\tChop Down Trees!\n"
                                                         + "\tDiscover Golden Eggs!\n"
                                                        + "You need " + Player.PLAYER_EGG_GOAL + " eggs to win.\nLevels are randomly generated.\n"
                                                        + "Press [Escape] to pause and view the instructions during gameplay.", new Vector2(10, 100), Color.White);
                        spriteBatch.DrawString(myFont, "Liam Middlebrook, Alec Linder", new Vector2(476, 570), Color.White);
                        spriteBatch.End();
                        break;
                    }

                case MenuScreenType.GAMEPLAY:
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

                        spriteBatch.Begin();
                        spriteBatch.DrawString(myFont, "[WASD] or Arrow Keys to move.\n"
                                                        + "[Space Bar] to interact with things and chop down trees.\n\n"
                                                        + "Collect eggs by doing various tasks.\n"
                                                        + "Collect more eggs by completing secret acheivments!\n"
                                                         + "\tInteract with Villagers!\n"
                                                         + "\tChop Down Trees!\n"
                                                         + "\tDiscover Golden Eggs!\n"
                                                        + "You need " + Player.PLAYER_EGG_GOAL + " eggs to win.\nLevels are randomly generated.\n"
                                                        + "Press [Escape] to pause the game during gameplay.", new Vector2(10, 100), Color.White);
                        spriteBatch.DrawString(myFont, "Liam Middlebrook, Alec Linder", new Vector2(476, 570), Color.White);
                        spriteBatch.End();
                        break;
                    }

                case MenuScreenType.CREDITS_MENU:
                    {

                        break;
                    }
                case MenuScreenType.WIN_MENU:
                    {
                        spriteBatch.Begin();
                        spriteBatch.DrawString(myFont, string.Format("You beat the game in {0:mms}!", TimeInMillisecondsToString(timeElaspsed)), new Vector2(20, 100), Color.White);
                        spriteBatch.End();
                        break;
                    }
                case MenuScreenType.LOSE_MENU:
                    {
                        spriteBatch.Begin();
                        bool outOfTime = timeElaspsed == (int)Player.PLAYER_ROUND_TIME;
                        string loseString = string.Empty;
                        if (outOfTime)
                        {
                            loseString += string.Format("You took {0:mms}!\nAnd died due to being attacked!", TimeInMillisecondsToString(timeElaspsed));
                        }
                        else
                        {
                            loseString += string.Format("You ran out of time!\nYou had {0} health remaining!", Player.Health);
                        }
                        spriteBatch.DrawString(myFont, loseString, new Vector2(20, 100), Color.White);
                        spriteBatch.End();
                        break;
                    }
            }


            spriteBatch.Begin();

            MenuSystem.GetInstance().DrawOverlay(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static string TimeInMillisecondsToString(double time)
        {
            double seconds = time / 1000.0;
            time %= 1000.0;

            double minutes = seconds / 60.0;
            seconds %= 60.0;

            return string.Format("{0:0} minutes and {1:00} seconds", minutes, seconds);
        }
    }
}
