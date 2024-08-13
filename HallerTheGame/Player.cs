//Author: Saadiq Shahsamand & Noah Segal
//File Name: Level.cs
//Project Name: HallerTheGame
//Creation Date: Apr. 19 2024
//Modified Date: Aug. 12 2024
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

namespace HallerTheGame
{
    class Player
    {
        public enum playerState { idle, walk, jump, sprint, sneak, roll, guard, attack }

        private playerState state = playerState.idle;

        public const int RIGHT = 1;
        public const int LEFT = -1;

        private int direction = RIGHT;

        private int health = 100;
        private int speed;

        private Texture2D[] animSprites;
        private Animation[] anims;

        private Timer[] abilityCooldowns;
        private bool[] abilityUnlocked;

        private bool isJumping = false;

        private Timer rollTimer;

        public Player()
        {

        }

        public int GetState()
        {
            return (int)state;
        }

        public void SetState(playerState state)
        {
            this.state = state;
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetHealth(int health)
        {
            this.health = health;
        }

        public void Update(GameTime gameTime, KeyboardState kb, KeyboardState prevKb, MouseState mouse, MouseState prevMouse)
        {
            for (int i = 0; i < anims.Length; i++)
            {
                anims[i].Update(gameTime);
            }

            /////////////
            /// INPUT ///
            /////////////
            
            //Check left right movement
            if (kb.IsKeyDown(Keys.D))
            {
                direction = RIGHT;
            }
            else if (kb.IsKeyDown(Keys.A))
            {
                direction = LEFT;
            }

            if ((kb.IsKeyDown(Keys.A) || kb.IsKeyDown(Keys.D)) && !isJumping)
            {
                //Check sprint or crouch
                if (kb.IsKeyDown(Keys.LeftShift))
                {
                    state = playerState.sprint;
                }
                else if (kb.IsKeyDown(Keys.LeftControl))
                {
                    state = playerState.sneak;
                }
                else
                {
                    state = playerState.walk;
                }
            }
            else if (kb.IsKeyDown(Keys.Space) && !prevKb.IsKeyDown(Keys.Space))
            {
                state = playerState.jump;
            }
            else if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton != ButtonState.Pressed)
            {
                state = playerState.attack;
            }
            else if (kb.IsKeyDown(Keys.S))
            {
                state = playerState.roll;
            }
            else
            {
                if (!isJumping)
                {
                    state = playerState.idle;
                }
            }

            //Move player
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            anims[(int)state].Draw(spriteBatch, Color.White);
        }
    }
}
