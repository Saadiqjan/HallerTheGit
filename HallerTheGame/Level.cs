//Author: Saadiq Shahsamand
//File Name: Level.cs
//Project Name: HallerTheGame
//Creation Date: Apr. 19 2024
//Modified Date: Jun. 22 2024
//Description: level
using GameUtility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HallerTheGame
{
    class Level
    {
        private StreamReader inFile;
        private StreamWriter outFile;

        private Song bgm;

        private Texture2D[] tileImgs;
        private Tile[,] tiles;

        //Store time passed in level
        private Timer levelTimer;

        public Level(Rectangle levelBounds, string mapFile, Song bgm)
        {
            LoadMap(mapFile);

            //Setup level timer
            levelTimer = new Timer(Timer.INFINITE_TIMER, true);
        }

        public void Update(GameTime gameTime)
        {
            //Update level timer
            levelTimer.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            

            spriteBatch.End();

            //Draw tiles
            for (int i = 0; i < tiles.GetLongLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLongLength(1); j++)
                {
                    tiles[i, j].Draw(spriteBatch);
                }
            }
        }

        private void LoadMap(string mapFile)
        {
            //Store read in lines
            string[] data;
            string line;

            line = inFile.ReadLine();
            data = line.Split(',');

            //Store does collide
            bool doesCollide = false;

            //Store tileset
            tiles = new Tile[Convert.ToInt32(data[0]), Convert.ToInt32(data[1])];

            //Try to read in tileset file
            try
            {
                //Open file
                inFile = File.OpenText(mapFile);

                //Loop through each row
                for (int i = 0; i < tiles.GetLongLength(0); i++)
                {
                    //Read in and split line
                    line = inFile.ReadLine();
                    data = line.Split(',');

                    //Add new tile for every coloumn in row
                    for (int j = 0; j < tiles.GetLongLength(1); j++)
                    {
                        //split data 
                        if (data[j][0].Equals('0'))
                        {
                            doesCollide = false;
                        }
                        else if (data[j][0].Equals('1'))
                        {
                            doesCollide = true;
                        }

                        data[j].Substring(1);

                        tiles[i, j] = new Tile(tileImgs[Convert.ToInt32(data[j])], Convert.ToInt32(data[j]), Tile.TILE_LENGTH * j, Tile.TILE_LENGTH * i, doesCollide);
                    }
                }
            }
            catch (Exception e)
            {
                //Write error message to console
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Close file if not null
                if (inFile != null)
                {
                    inFile.Close();
                }
            }
        }
    }
}
