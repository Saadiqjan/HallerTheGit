//Author: Saadiq Shahsamand & Noah Segal
//File Name: Game1.cs
//Project Name: HallerTheGame
//Creation Date: Mar. 18 2024
//Modified Date: Aug. 12 2024
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

        private const int NEW_GAME_BTN = 0;
        private const int CONTINUE_BTN = 1;
        private const int EXIT_BTN = 2;

        private const int NUM_TILES = 3;

        public static Random rng = new Random();

        private Cam2D cam;

        //Store Window width and height
        public static int screenWidth;
        public static int screenHeight;

        private MouseState mouse;
        private MouseState prevMouse;

        private KeyboardState kb;
        private KeyboardState prevKb;

        private bool tutorialDone = true;

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

        private Vector2[,] saveBtnPos = new Vector2[3, 3];
        private Button[,] saveBtns = new Button[3, 3];

        private Texture2D[] tileImgs = new Texture2D[NUM_TILES];

        private Level tutorialLevel;

        private Camp playerCamp;

        //An enum is a variable in c# that essentially replaces states
        //Essentially you make your own variable and set it
        public enum gameState
        {
            menu, saveMenu, tutorial, camp, levelSelect, level,
            pause, levelComplete
        }

        //Define the state that the game is currently in
        private gameState state = gameState.menu;

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

            graphics.PreferredBackBufferWidth = 1152;
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

            tileImgs[Tile.AIR] = null;
            tileImgs[Tile.GRASS] = Content.Load<Texture2D>("Images/Sprites/GrassBlock");
            tileImgs[Tile.DIRT] = Content.Load<Texture2D>("Images/Sprites/DirtBlock");

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

            playerCamp = new Camp(tileImgs, new Rectangle(0, 0, screenWidth, screenHeight), "CampFile.txt", null);
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
            switch (state)
            {
                case gameState.menu:
                    MainMenuUpdate();
                    break;
                case gameState.saveMenu:
                    SaveMenuUpdate();
                    break;
                case gameState.tutorial:
                    break;
                case gameState.camp:
                    CampUpdate();
                    break;
                case gameState.levelSelect:
                    break;
                case gameState.level:
                    break;
                case gameState.pause:
                    break;
                case gameState.levelComplete:
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

            switch (state)
            {
                case gameState.menu:
                    MainMenuDraw();
                    break;
                case gameState.saveMenu:
                    SaveMenuDraw();
                    break;
                case gameState.tutorial:
                    break;
                case gameState.camp:
                    CampDraw();
                    break;
                case gameState.levelSelect:
                    break;
                case gameState.level:
                    break;
                case gameState.pause:
                    break;
                case gameState.levelComplete:
                    break;
            }

            base.Draw(gameTime);
        }

        private void MainMenuUpdate()
        {
            menuBG.Update();

            if (menuBtns[NEW_GAME_BTN].IsClicked(mouse, prevMouse))
            {
                state = gameState.saveMenu;
            }
            else if (menuBtns[CONTINUE_BTN].IsClicked(mouse, prevMouse))
            {
                state = gameState.saveMenu;
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
                            state = gameState.camp;
                        }
                        else
                        {
                            state = gameState.tutorial;
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

        private void CampUpdate()
        {

        }

        private void CampDraw()
        {
            playerCamp.Draw(spriteBatch);
        }
    }
}
