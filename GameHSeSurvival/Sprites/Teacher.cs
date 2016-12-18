using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    class Teacher : Sprite
    {
        public Teacher(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
            : base(texture, position, spritebatch)
        {
        }
        #region CheckCollision
        public delegate void DeleteTeacher(Teacher teacher);
        public event DeleteTeacher DeleteTeacherEvent;
        
        public override bool Collision(Player player)
        {
                if (this.HurtOrKilledBy(player)[0])
                {
                DeleteTeacherEvent?.Invoke(this);
                player.Movements.move -= Vector2.UnitY * 25f;
                player.Score += 5;
                return true;
                }
                if (this.HurtOrKilledBy(player)[1])
                {
                player.Sprite_vector = new Vector2(550, 576 - player.Sprite_texture.Height);
                return false;
                }
                if (this.HurtOrKilledBy(player)[0] == false && this.HurtOrKilledBy(player)[1] == false)
                {
                return false; }
            return false;
        }
        #endregion
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

                if (player.rectangle.X < rectangle.X)
                {
                    if (onePixelDown.Intersects(rectangle) &&
                        player.rectangle.Bottom >= rectangle.Top && // пересеклись сверху
                        (player.rectangle.X >= rectangle.X - rectangle.Width || // наехал на учителя слева
                        (player.rectangle.X >= rectangle.X && player.rectangle.X <= rectangle.X + rectangle.Width)) && // наехал на учителя справа
                        ((fourPixelLeft.Right - rectangle.Left) >= 4)) // а учитель по бокам-то всё видит
                    {
                        WhatToDo[0] = true;
                    }
                }

                if (player.rectangle.X > rectangle.X)
                {
                    if (onePixelDown.Intersects(rectangle) &&
                        player.rectangle.Bottom >= rectangle.Top && // пересеклись сверху
                        (player.rectangle.X >= rectangle.X - rectangle.Width || // наехал на учителя слева
                        (player.rectangle.X >= rectangle.X && player.rectangle.X <= rectangle.X + rectangle.Width)) && // наехал на учителя справа
                        (Math.Abs((fourPixelRight.Left - rectangle.Right)) >= 4)) // а учитель по бокам-то всё видит
                    {
                        WhatToDo[0] = true;
                    }
                }

                if (player.rectangle.X == rectangle.X)
                {
                    if (onePixelDown.Intersects(rectangle) &&
                        player.rectangle.Bottom >= rectangle.Top && // пересеклись сверху
                        (player.rectangle.X >= rectangle.X - rectangle.Width || // наехал на учителя слева
                        (player.rectangle.X >= rectangle.X && player.rectangle.X <= rectangle.X + rectangle.Width)))
                    {
                        WhatToDo[0] = true;
                    }
                }

                if (WhatToDo[0] == false)
                {
                    WhatToDo[1] = true;
                }
            }
            return WhatToDo;
        }
        #endregion
    }
}
