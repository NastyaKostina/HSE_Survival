using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Block : Sprite
    {
        public bool blocked { get; set; }
        public bool moving { get; set; }
        public Vector2 speed { get; set; }
        public int startposition { get; set; }
        public int changeposition { get; set; }
        public int steps { get; set; }

        public Block(Texture2D Sprite_texture, Vector2 Sprite_vector, SpriteBatch sb, bool blocked,
            bool moving, int steps)
            : base(Sprite_texture, Sprite_vector, sb)
        {
            this.blocked = blocked;
            this.moving = moving;
            startposition = (int)Sprite_vector.X;
            changeposition = steps * Sprite_texture.Width;
            this.steps = steps;
        }

        public override void Draw()
        {
            if (blocked)
            { base.Draw(); }
        }
    }
}
