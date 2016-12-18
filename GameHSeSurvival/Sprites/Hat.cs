using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Hat:Sprite
    {
        public bool GotIt;
        public Hat(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
        : base(texture, position, spritebatch)
        {
            GotIt = false;
        }

        public override bool Collision(Player player)
        {
            if (player.rectangle.Intersects(rectangle))
            {
                GotIt = true;
                return true;
            }
            return false;
        }
    }
}
