using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    interface IRepository
    {
        void Collisisons(GameTime gametime);
        void SetValues(Dictionary<string, Texture2D> Values, SpriteBatch spriteBatch);
        void Draw(SpriteBatch spriteBatch, SpriteFont Font, GameTime gametime);
    }
}
