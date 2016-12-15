using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    interface ISprite
    {
        Texture2D Sprite_texture { get; set; }
        Vector2 Sprite_vector { get; set; }
        SpriteBatch Sb { get; set; }
        Rectangle rectangle { get; }
    }
}
