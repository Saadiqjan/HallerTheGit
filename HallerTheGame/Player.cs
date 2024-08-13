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
        public enum playerState { idle, walk, jump, sneak, roll, guard, attack }

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

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
