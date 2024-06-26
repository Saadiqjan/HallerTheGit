//Author: Saadiq Shahsamand
//File Name: Game1.cs
//Project Name: HallerTheGame
//Creation Date: Mar. 18 2024
//Modified Date: Apr. 20 2024
//Description: Funny haha game
using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HallerTheGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public const int MAIN_MENU = 0;
        public const int SAVE_MENU = 1;
        public const int TUTORIAL = 2;
        public const int CAMP = 3;
        public const int LEVEL_SELECT = 4;
        public const int LEVEL = 5;
        public const int PAUSE = 6;
        public const int LEVEL_COMPLETE = 7;
        public static int gameState = MAIN_MENU;

        private const int NEW_GAME_BTN = 0;
        private const int CONTINUE_BTN = 1;
        private const int EXIT_BTN = 2;

        public static Random rng = new Random();

        private Cam2D cam;

        //Store Window width and height
        public static int screenWidth;
        public static int screenHeight;

        private MouseState mouse;
        private MouseState prevMouse;

        private KeyboardState kb;
        private KeyboardState prevKb;

        private bool tutorialDone = false;

        private SpriteFont buttonFont;

        private SoundEffect buttonClick;

        private Texture2D titleImg;
        private Rectangle titleRec;

        private Texture2D[] bgLayers = new Texture2D[7];
        private Parallax menuBG;

        private Texture2D btnImg;
        private Texture2D saveBtnImg;

        private Vector2[] menuBtnPos = new Vector2[3];
        private Button[] menuBtns = new Button[3];

        private Vector2[,] saveBtnPos = new Vector2[3,3];
        private Button[,] saveBtns = new Button[3, 3];

        private Level tutorialLevel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 672;

            //Apply changes
            graphics.ApplyChanges();

            //Store screen dimensions
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

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

            int num = rng.Next(0, 100);

            if (num == 57)
            {
                titleImg = Content.Load<Texture2D>("Images/Sprites/HallerTitle2");
            }
            else
            {
                titleImg = Content.Load<Texture2D>("Images/Sprites/HallerTitle");
            }

            buttonFont = Content.Load<SpriteFont>("Fonts/ButtonFont");

            buttonClick = Content.Load<SoundEffect>("Audio/SFX/ButtonClick");

            bgLayers[0] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_8");
            bgLayers[1] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_7");
            bgLayers[2] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_6");
            bgLayers[3] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_5");
            bgLayers[4] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_4");
            bgLayers[5] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_3");
            bgLayers[6] = Content.Load<Texture2D>("Images/Backgrounds/Starry_night_Layer_2");

            btnImg = Content.Load<Texture2D>("Images/Sprites/MenuButton");
            saveBtnImg = Content.Load<Texture2D>("Images/Sprites/SaveButton");

            titleRec = new Rectangle(screenWidth / 2 - titleImg.Width / 2, 50, titleImg.Width, titleImg.Height);

            for (int i = 0; i < menuBtns.Length; i++)
            {
                menuBtnPos[i] = new Vector2(screenWidth / 2 - (int)(btnImg.Width * 0.75f), titleRec.Bottom + 50 + 100 * i);
            }

            int xOff = screenWidth / (saveBtns.GetLength(0) + 1) - saveBtnImg.Width;
            int yOff = screenHeight / (saveBtns.GetLength(1) + 1) - saveBtnImg.Height;

            int saveNum = 0; 

            for (int i = 0; i < saveBtns.GetLength(0); i++)
            {
                for (int j = 0; j < saveBtns.GetLength(1); j++)
                {
                    saveNum++;
                    saveBtnPos[j, i] = new Vector2(xOff * (j + 1) + saveBtnImg.Width * j, yOff * (i + 1) + saveBtnImg.Height * i);
                    saveBtns[j, i] = new Button(saveBtnImg, buttonFont, "Save " + saveNum, Color.White, Color.LightGray, saveBtnPos[j, i], 2.5f, buttonClick);
                }
            }

            menuBtns[NEW_GAME_BTN] = new Button(btnImg, buttonFont, "New Game", Color.White, Color.LightGray, menuBtnPos[NEW_GAME_BTN], 1.5f, buttonClick);
            menuBtns[CONTINUE_BTN] = new Button(btnImg, buttonFont, "Continue", Color.White, Color.LightGray, menuBtnPos[CONTINUE_BTN], 1.5f, buttonClick);
            menuBtns[EXIT_BTN] = new Button(btnImg, buttonFont, "Exit", Color.White, Color.LightGray, menuBtnPos[EXIT_BTN], 1.5f, buttonClick);

            menuBG = new Parallax(bgLayers, 0, 0.45f);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            prevMouse = mouse;
            mouse = Mouse.GetState();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case MAIN_MENU:
                    MainMenuUpdate();
                    break;
                case SAVE_MENU:
                    SaveMenuUpdate();
                    break;
                case TUTORIAL:
                    break;
                case CAMP:
                    break;
                case LEVEL_SELECT:
                    break;
                case LEVEL:
                    break;
                case PAUSE:
                    break;
                case LEVEL_COMPLETE:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (gameState)
            {
                case MAIN_MENU:
                    MainMenuDraw();
                    break;
                case SAVE_MENU:
                    SaveMenuDraw();
                    break;
                case TUTORIAL:
                    break;
                case CAMP:
                    break;
                case LEVEL_SELECT:
                    break;
                case LEVEL:
                    break;
                case PAUSE:
                    break;
                case LEVEL_COMPLETE:
                    break;
            }

            base.Draw(gameTime);
        }

        private void MainMenuUpdate()
        {
            menuBG.Update();

            if (menuBtns[NEW_GAME_BTN].IsClicked(mouse, prevMouse))
            {
                gameState = SAVE_MENU;
            }
            else if (menuBtns[CONTINUE_BTN].IsClicked(mouse, prevMouse))
            {
                gameState = SAVE_MENU;
            }
            else if (menuBtns[EXIT_BTN].IsClicked(mouse, prevMouse))
            {
                Exit();
            }
        }

        private void MainMenuDraw()
        {
            spriteBatch.Begin();
            menuBG.Draw(spriteBatch);
            spriteBatch.Draw(titleImg, titleRec, Color.White);

            for (int i = 0; i < menuBtns.Length; i++)
            {
                menuBtns[i].Draw(spriteBatch, mouse.Position);
            }
            
            spriteBatch.End();
        }

        private void SaveMenuUpdate()
        {
            menuBG.Update();

            for (int i = 0; i < saveBtns.GetLongLength(0); i++)
            {
                for (int j = 0; j < saveBtns.GetLongLength(1); j++)
                {
                    if (saveBtns[i, j].IsClicked(mouse, prevMouse))
                    {
                        if (tutorialDone)
                        {
                            gameState = CAMP;
                        }
                        else
                        {
                            gameState = TUTORIAL;
                        }
                    }
                }
            }
        }

        private void SaveMenuDraw()
        {
            spriteBatch.Begin();
            menuBG.Draw(spriteBatch);

            for (int i = 0; i < saveBtns.GetLongLength(0); i++)
            {
                for (int j = 0; j < saveBtns.GetLongLength(1); j++)
                {
                    saveBtns[i, j].Draw(spriteBatch, mouse.Position);
                }
            }

            spriteBatch.End();
        }
    }
}
