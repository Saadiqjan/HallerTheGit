//Author: Saadiq Shahsamand
//File Name: Tile.cs
//Project Name: HallerTheGame
//Creation Date: Apr. 20 2024
//Modified Date: Apr. 20 2024
//Description: a tile
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

namespace HallerTheGame
{
    class Tile
    {
        //Store tile length
        public const int TILE_LENGTH = 32;

        //Tile ids as constant ints
        public const int AIR = 0;
        public const int GRASS = 1;
        public const int DIRT = 2;

        //Store tile id
        private int tileID;

        //Store image, rectanglem and position of tile
        private Texture2D tileImg;
        private Vector2 tilePos;
        private Rectangle tileRec;

        //Check if tile collides
        private bool doesCollide;

        /// <summary>
        /// Create tile
        /// </summary>
        /// <param name="tileImg">tile image</param>
        /// <param name="tileID">tile id</param>
        /// <param name="xPos">x position</param>
        /// <param name="yPos">y position</param>
        public Tile(Texture2D tileImg, int tileID, int xPos, int yPos, bool doesCollide)
        {
            //Store parameters
            this.tileImg = tileImg;
            this.tileID = tileID;
            this.doesCollide = doesCollide;

            //Create rectangle and position of tile
            tilePos = new Vector2(xPos, yPos);
            tileRec = new Rectangle(xPos, yPos, TILE_LENGTH, TILE_LENGTH);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tileImg == null) return;

            spriteBatch.Draw(tileImg, tileRec, Color.White);
        }
    }
}
