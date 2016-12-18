using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameHSeSurvival
{
    
    class Player:Sprite
    {
        public PlayerMovementSystem Movements = new PlayerMovementSystem();
        private Vector2 move;
        public int Score { get; set; }  
        
        public Player(Texture2D texture, Vector2 position, SpriteBatch spritebatch) //List<SoundEffect> sounds)
        : base(texture, position, spritebatch)
        {
            move = Movements.move;
        }
        public void Update(GameTime gameTime)
        {
            Movements.Move(gameTime, this);
        }
    }
}
