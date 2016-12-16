using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class BombQuestion:Sprite
    {
        Random random = new Random();
        bool IsRemoved;
        public BombQuestion(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
            : base(texture, position, spritebatch)
        {
            position = new Vector2(random.Next(0, 960 - texture.Width), -640);
        }

    }
}
