//Author: Saadiq Shahsamand
//File Name: Button.cs
//Project Name: HallerTheGame
//Creation Date: Mar. 24 2024
//Modified Date: Mar. 24 2024
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
    class Button
    {
        //Store image and image 
        private Texture2D img;

        //Store rectangle and position
        private Rectangle rec;
        private Vector2 pos;

        //Store click sound effect
        private SoundEffect click;

        //Store text and colour
        private SpriteFont font;
        private string text;
        private Color colour;
        private Color hoverColour;

        /// <summary>
        /// Create a button
        /// </summary>
        /// <param name="img">button image</param>
        /// <param name="font">button text font</param>
        /// <param name="text">button text</param>
        /// <param name="colour">button text colour</param>
        /// <param name="hoverColour">button text hover colour</param>
        /// <param name="pos">button position</param>
        /// <param name="scale">button scale</param>
        /// <param name="click">button click sound effect</param>
        public Button(Texture2D img, SpriteFont font, string text, Color colour, Color hoverColour, Vector2 pos, float scale, SoundEffect click)
        {
            //Store parameters
            this.img = img;
            this.font = font;
            this.text = text;
            this.colour = colour;
            this.hoverColour = hoverColour;
            this.pos = pos;
            this.click = click;

            //Create rectangle
            rec = new Rectangle((int)pos.X, (int)pos.Y, (int)(img.Width * scale), (int)(img.Height * scale));
        }

        /// <summary>
        /// Get button positon
        /// </summary>
        /// <returns>button positon</returns>
        public Vector2 GetPos()
        {
            return pos;
        }

        /// <summary>
        /// Get button rectangle
        /// </summary>
        /// <returns>button rectangle</returns>
        public Rectangle GetRec()
        {
            return rec;
        }

        /// <summary>
        /// Set position of button
        /// </summary>
        /// <param name="pos">new position</param>
        public void SetPos(Vector2 pos)
        {
            //Set positon vector and rectangle position
            this.pos = pos;
            rec.X = (int)pos.X;
            rec.Y = (int)pos.Y;
        }

        /// <summary>
        /// Check if button has been clicked
        /// </summary>
        /// <param name="mouse">current mouse state</param>
        /// <param name="prevMouse">previous mouse state</param>
        /// <returns>If button has been clicked</returns>
        public bool IsClicked(MouseState mouse, MouseState prevMouse)
        {
            //Check if mouse has been clicked
            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                //Check if mouse positon is in button rec
                if (rec.Contains(mouse.Position))
                {
                    //Play sound effect
                    click.CreateInstance().Play();

                    //Return true
                    return true;
                }
            }

            //Return false
            return false;
        }

        /// <summary>
        /// Draw button with hover image
        /// </summary>
        /// <param name="spriteBatch">for drawing image</param>
        /// <param name="mousePoint">position of mouse</param>
        public void Draw(SpriteBatch spriteBatch, Point mousePoint)
        {
            //Calculate half of string height and length
            int halfStringLength = (int)(font.MeasureString(text).X * 0.5f);
            int halfStringHeight = (int)(font.MeasureString(text).Y * 0.5f);

            //Draw button
            spriteBatch.Draw(img, rec, Color.White);

            //Check if mouse is over button
            if (rec.Contains(mousePoint))
            {
                spriteBatch.DrawString(font, text, new Vector2(rec.Center.X - halfStringLength, rec.Center.Y - halfStringHeight), hoverColour);
            }
            else
            {
                spriteBatch.DrawString(font, text, new Vector2(rec.Center.X - halfStringLength, rec.Center.Y - halfStringHeight), colour);
            }
        }
    }
}
