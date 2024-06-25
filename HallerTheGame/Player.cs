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

namespace HallerTheGame
{
    class Player
    {
        public const int IDLE = 0;
        public const int WALK = 1;
        public const int JUMP = 2;
        public const int SNEAK = 3;
        public const int ROLL = 4;
        public const int GUARD = 5;
        public const int ATTACK = 6;

        private int state = IDLE;

        public const int RIGHT = 1;
        public const int LEFT = -1;

        private int direction = RIGHT;

        private int health = 100;
        private int speed;

        private Texture2D[] animSprites;
        private Animation[] anims;

        private Timer[] abilityCooldowns;
        private bool[] abilityUnlocked;

        public Player()
        {

        }

        public int GetState()
        {
            return state;
        }

        public void SetState(int state)
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

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
