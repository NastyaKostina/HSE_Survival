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
        public delegate void DeleteBomb(BombQuestion bombquestion);
        public event DeleteBomb DeleteBombEvent;
        public Vector2 Speed { get; set; }
        Random random = new Random();
        public BombQuestion(Texture2D texture, Vector2 position, SpriteBatch spritebatch)
            : base(texture, position, spritebatch)
        {
            this.Sprite_vector = new Vector2((random.Next(3008, 3456)),0);
            Speed = new Vector2(0, 0.5f);
        }
        public override bool Collision(Player player, GameTime gametime)
        {
            if (this.rectangle.Bottom >= 576)
            {
                DeleteBombEvent?.Invoke(this);
                return true;
            }
            if (player.rectangle.Intersects(this.rectangle))
            {
                player.Sprite_vector = new Vector2(550, 576 - player.Sprite_texture.Height);
                player.Score = 0;
                return false;
            }
            return false;
        }
        public void Update(GameTime gametime)
        {
            this.Sprite_vector += Speed * (float)gametime.ElapsedGameTime.TotalMilliseconds / 15;
        }
    }
}
