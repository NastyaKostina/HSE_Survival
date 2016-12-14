using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Sprite
    {
        public Texture2D Sprite_texture { get; set; }
        public Vector2 Sprite_vector { get; set; }
        public SpriteBatch Sb { get; set; }

        public Sprite(Texture2D sprite_texture, Vector2 sprite_vector, SpriteBatch sb)
        {
            Sprite_vector = sprite_vector;
            Sprite_texture = sprite_texture;
            Sb = sb;
        }

        public virtual void Draw()
        {
            Sb.Draw(Sprite_texture, Sprite_vector, Color.White);
        }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)Sprite_vector.X, (int)Sprite_vector.Y,
                          Sprite_texture.Width, Sprite_texture.Height);
            }
        }
        #region Collision
        public bool[] HurtOrKilledBy(Player player)
        {
            bool[] WhatToDo = new bool[2];
            WhatToDo[0] = false; WhatToDo[1] = false; // WhatToDo[0] - KILL THE TEACHER; WhatToDo[1] - KILL THE PLAYER
            Rectangle onePixelDown = player.rectangle;
            onePixelDown.Offset(0, 1);
            if (onePixelDown.Intersects(rectangle))
            {
                Rectangle fourPixelLeft = player.rectangle;
                Rectangle fourPixelRight = player.rectangle;
                fourPixelLeft.Offset(-4, 0);
                fourPixelRight.Offset(4, 0);
                if (onePixelDown.Intersects(rectangle) &&
                    player.rectangle.Bottom >= rectangle.Top && // пересеклись сверху
                    (player.rectangle.X >= rectangle.X - rectangle.Width || // наехал на учителя слева
                    (player.rectangle.X >= rectangle.X && player.rectangle.X <= rectangle.X + rectangle.Width)) && // наехал на учителя справа
                    ((fourPixelLeft.Right - rectangle.Left) >= 4 && (fourPixelRight.Left - rectangle.Right <= 4))) // а учитель по бокам-то всё видит
                {
                    WhatToDo[0] = true;
                }
                else// if (onePixelDown.Intersects(rectangle) && !(player.rectangle.Bottom >= rectangle.Top))
                {
                    WhatToDo[1] = true;
                }
            }
            return WhatToDo;
        }
    #endregion
}
}
