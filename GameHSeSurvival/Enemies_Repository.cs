using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Enemies_Repository
    {
        public List<Teacher> Teachers = new List<Teacher>();
        public void DrawTeacher(SpriteBatch sb)
        {
            for (int i = 0; i < Teachers.Count(); i++)
            {
                Teachers[i].Draw();
            }
        }
        public void Collisions(Player pl)
        {
            for (int i = 0; i < Teachers.Count(); i++)
            {
                if (Teachers[i].IsTop(pl))
                { Teachers.RemoveAt(i); pl.move -= Vector2.UnitY * 25f; }
                if (Teachers[i].IsLaterally(pl)) pl.Sprite_vector = new Vector2(550, 576 - pl.Sprite_texture.Height);
            }
        }
    }
}
