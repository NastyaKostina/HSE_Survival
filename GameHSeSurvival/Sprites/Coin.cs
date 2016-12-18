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

        public bool exist;
        public Coin(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
            : base(texture, position, spritebatch)
        {
            exist = false;
        }
        public override bool Collision(Player player)
        {
                if (player.rectangle.Intersects(this.rectangle))
                {
                 DeleteCoinEvent?.Invoke(this);
                player.Score += 1;
                exist = false;
                return true;
            }
            return false;
        }

        public override void Draw()
        {
            if (exist)
            { base.Draw(); }
        }
    }
}
