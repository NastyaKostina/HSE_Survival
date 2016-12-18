using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    
    class Repository : IRepository
    {
        
        const int ground_level = 576;
        
        public Player Player { get; set; }
        public Board Board { get; set; }
        public Hat Hat { get; set; }
        public BombQuestion Bomb { get; set; }

        private IList<Coin> _Coins = new List<Coin>();
        public IList<Coin> Coins
        {
            get { return _Coins; }
            set { _Coins = value; }
        }
        
        private IList<Teacher> _Teachers = new List<Teacher>();
        public IList<Teacher> Teachers
        {
            get { return _Teachers; }   
            set { _Teachers = value; }
        }
        
        private IList<BombQuestion> _Bombs = new List<BombQuestion>();
        public IList<BombQuestion> Bombs
        {
            get { return _Bombs; }
            set { _Bombs  = value; }
        }

        double timer;
        public double Timer
        {
            get
            {
                return Math.Floor(timer);
            }
        }

        Texture2D bomb_texture;
        SpriteBatch spriteBatch;
        
        public void SetValues(Dictionary<string, Texture2D> Values, SpriteBatch spriteBatch)
        {
            Player = new Player(Values["студент"], new Vector2(448, ground_level - Values["студент"].Height), spriteBatch);
            Board = new Board(spriteBatch, Values["блок"], Values["монетка"], 87, 10);
            foreach (var coin in Board.coins)
            {
                if (coin.exist)
                    _Coins.Add(coin);
            }

            Teachers.Add(new Teacher(Values["учитель"], new Vector2(3840, ground_level - Values["учитель"].Height), spriteBatch));
            Teachers.Add(new Teacher(Values["учительница"], new Vector2(3200, ground_level - Values["учительница"].Height), spriteBatch));
            Teachers.Add(new Teacher(Values["учительница"], new Vector2(4672, ground_level - Values["учительница"].Height - 192), spriteBatch));
            Hat = new Hat(Values["шапочка"], new Vector2(85*64, ground_level - Values["шапочка"].Height), spriteBatch);
            this.bomb_texture = Values["бомба"];
            this.spriteBatch = spriteBatch;
            Player.Score = 0;
            
        }
        
        public void Collisisons(GameTime gametime)
        {
            foreach (var item in Teachers)
            {
                item.DeleteTeacherEvent += e => Teachers.Remove(e);
                if (item.Collision(Player))
                {
                    break;
                }
                 this.Start(bomb_texture, gametime, spriteBatch);  
            }
            foreach (var item in Coins)
            {
                item.DeleteCoinEvent += e => Coins.Remove(e);
                if(item.Collision(Player)) break;
            }
            foreach (var item in Bombs)
            {
                item.DeleteBombEvent += e => Bombs.Remove(e);
                if (item.Collision(Player)) break;
            }
            
        }
        
        Random random = new Random();
        float spawn;

        public void Start(Texture2D bomb_texture, GameTime gametime, SpriteBatch spritebatch)
        {
            if (Teachers.Count != 0)
            {
                int randx = random.Next(832, 4928);
               spawn += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (spawn >= 1)
                {
                    spawn = 0;
                    if (Bombs.Count() < 30)
                    {
                        Bombs.Add(new BombQuestion(bomb_texture, new Vector2(randx, 0), spritebatch));
                    }
                }
            }
            else Bombs.Clear();
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont Font, GameTime gameTime)
        {
            Player.Draw();
            Board.Draw();
            for (int i = 0; i < Teachers.Count(); i++)
            {
                Teachers[i].Draw();
            }
            foreach (var item in Coins)
            {
                item.Draw();
            }
            foreach (var item in Bombs)
            {
                item.Draw();
            }
            
            Hat.Draw();
            spriteBatch.DrawString(Font, Convert.ToString("NAKOPLENNAYA: " + 0.1 * Player.Score), new Vector2(Player.Sprite_vector.X - 30, Player.Sprite_vector.Y - 30), Color.Azure);//new Vector2(Player.Sprite_vector.X - 7*64, 64), Color.Azure);           
            spriteBatch.DrawString(Font, Convert.ToString("Time: " + Math.Floor(timer += gameTime.ElapsedGameTime.TotalSeconds)), new Vector2(Player.Sprite_vector.X - 12, Player.Sprite_vector.Y - 60), Color.Azure);
        }
    }
}
