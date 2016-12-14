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
        public bool IsLaterally(Player player)
        {
            // return (rectangle.Intersects(player.rectangle) && Sprite_vector.Y != player.Sprite_vector.Y + player.Sprite_texture.Height);
            Rectangle onePixelDown = player.rectangle;
            if (onePixelDown.Intersects(rectangle) && !((int)player.Sprite_vector.Y + player.rectangle.Height == (int)Sprite_vector.Y))
                return true;
            else return false;
        }
        public bool IsTop(Sprite player)
        {
            // return Sprite_vector.Y == player.Sprite_vector.Y +  player.Sprite_texture.Height && rectangle.Intersects(player.rectangle);
            Rectangle onePixelDown = player.rectangle;
            Rectangle onePixelLeft = player.rectangle;
            Rectangle onePixelRight = player.rectangle;
            onePixelDown.Offset(0, 1);
            //  onePixelLeft.Offset(-1, 0);
            // onePixelRight.Offset(1, 0);
            if (onePixelDown.Intersects(rectangle) && (int)player.Sprite_vector.Y + player.rectangle.Height == (int)Sprite_vector.Y) //&& !onePixelLeft.Intersects(rectangle) && !onePixelRight.Intersects(rectangle))
                return true;
            else
                return false;
        }
        #endregion
    }
}
