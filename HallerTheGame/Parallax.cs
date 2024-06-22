//Author: Saadiq Shahsamand
//File Name: Parallax.cs
//Project Name: HallerTheGame
//Creation Date: Mar. 23 2024
//Modified Date: Mar. 23 2024
//Description: Parallax background
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
    class Parallax
    {
        //Store background layer images
        private Texture2D[] bgLayerImgs;

        //Store background layer positions and rectangles
        private Vector2[] bgLayerPos1;
        private Vector2[] bgLayerPos2;
        private Rectangle[] bgLayerRecs1;
        private Rectangle[] bgLayerRecs2;

        //Store minimum, maxmium, and speed increase rate
        private float minSpeed;
        private float maxSpeed;
        private float speedIncrease;

        /// <summary>
        /// Create parallax baxkground
        /// </summary>
        /// <param name="bgLayerImgs">background layer images</param>
        /// <param name="minSpeed">minimum speed</param>
        /// <param name="maxSpeed">maximum speed</param>
        public Parallax(Texture2D[] bgLayerImgs, float minSpeed, float maxSpeed)
        {
            //Store parameters
            this.bgLayerImgs = bgLayerImgs;
            this.minSpeed = minSpeed;
            this.maxSpeed = maxSpeed;

            //Calculate speed increase rate
            speedIncrease = (float)((maxSpeed - minSpeed) / bgLayerImgs.Length);

            //Create rectangles and positions for each layer
            bgLayerRecs1 = new Rectangle[bgLayerImgs.Length];
            bgLayerRecs2 = new Rectangle[bgLayerImgs.Length];
            bgLayerPos1 = new Vector2[bgLayerImgs.Length];
            bgLayerPos2 = new Vector2[bgLayerImgs.Length];

            //Create rectangles and positions for each layer
            for (int i = 0; i < bgLayerImgs.Length; i++)
            {
                bgLayerPos1[i] = new Vector2(0, 0);
                bgLayerRecs1[i] = new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight);
                bgLayerPos2[i] = new Vector2(Game1.screenWidth, 0);
                bgLayerRecs2[i] = new Rectangle(Game1.screenWidth, 0, Game1.screenWidth, Game1.screenHeight);
            }
        }

        /// <summary>
        /// Update parallax background
        /// </summary>
        public void Update()
        {
            //Loop through each menu background layer
            for (int i = 0; i < bgLayerRecs1.Length; i++)
            {
                //Move background layer by a speed relatice to layer number
                bgLayerPos1[i].X -= i * speedIncrease * 2 + minSpeed;
                bgLayerPos2[i].X -= i * speedIncrease * 2 + minSpeed;
                bgLayerRecs1[i].X = (int)bgLayerPos1[i].X;
                bgLayerRecs2[i].X = (int)bgLayerPos2[i].X;
            }

            //Loop through each menu background layer
            for (int i = 0; i < bgLayerRecs1.Length; i++)
            {
                //Check if layer goes off screen
                if (bgLayerRecs1[i].Right <= 0)
                {
                    //Place layer on other side of screen
                    bgLayerPos1[i].X = Game1.screenWidth;
                    bgLayerRecs1[i].X = Game1.screenWidth;
                }

                //Check if layer goes off screen
                if (bgLayerRecs2[i].Right <= 0)
                {
                    //Place layer on other side of screen
                    bgLayerPos2[i].X = Game1.screenWidth;
                    bgLayerRecs2[i].X = Game1.screenWidth;
                }
            }
        }

        /// <summary>
        /// Draw parallax background
        /// </summary>
        /// <param name="spriteBatch">for drawing images</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw each layer
            for (int i = 0; i < bgLayerImgs.Length; i++)
            {
                spriteBatch.Draw(bgLayerImgs[i], bgLayerRecs1[i], Color.White);
                spriteBatch.Draw(bgLayerImgs[i], bgLayerRecs2[i], Color.White);
            }
        }
    }
}
