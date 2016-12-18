using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Coin:Sprite
    {
        public delegate void DeleteCoin(Coin coin);
        public event DeleteCoin DeleteCoinEvent;
        public Coin(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
            : base(texture, position, spritebatch)
        {
        }
        public override bool Collision(Player player)
        {
                if (player.rectangle.Intersects(this.rectangle))
                {
                DeleteCoinEvent?.Invoke(this);
                player.Score += 1;
                return true;
            }
            return false;
        }
    }
}
