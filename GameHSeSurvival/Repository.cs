﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    
    class Repository
    {
        
        const int ground_level = 576;
        double timer;
        private BombQuestion _Bomb;

        public BombQuestion Bomb
        {
            get { return _Bomb; }   
        }

        public List<Teacher> _Teachers = new List<Teacher>();
        List<Coin> _Coins = new List<Coin>();
        Player _Player;
        Board _Board;
        Hat _Hat;
        List<BombQuestion> _Bombs = new List<BombQuestion>();

        public double Timer
        {
            get
            {
                return Math.Floor(timer);
            }
        }

        public List<Teacher> Teachers
        {
            get
            {
                return _Teachers;
            }
        }

        public List<Coin> Coins
        {
            get
            {
                return _Coins;
            }
        }
        public Player Player
        {
            get
            {
                return _Player;
            }
        }

        public Board Board
        {
            get
            {
                return _Board;
            }
        }

        public Hat Hat
        {
            get
            {
                return _Hat;
            }
        }
        
        public List<BombQuestion> Bombs
        {
            get { return _Bombs; }
        }


        int[,] coins = new int[87, 10];
        public void CoinsCoordinates()
        {
            coins[9, 1] = 1;
            coins[9, 3] = 1;
            coins[18, 5] = 1;
            coins[24, 2] = 1;
            coins[29, 2] = 1;
            coins[29, 3] = 1;
            coins[29, 4] = 1;
            coins[35, 1] = 1;
            coins[36, 5] = 1;
            coins[36, 6] = 1;
            coins[36, 7] = 1;
            coins[45, 8] = 1;
            coins[46, 8] = 1;
            coins[47, 8] = 1;
            coins[48, 8] = 1;
            coins[49, 5] = 1;
            coins[49, 6] = 1;
            coins[49, 7] = 1;
            coins[55, 4] = 1;
            coins[55, 5] = 1;
            coins[55, 6] = 1;
            coins[58, 7] = 1;
            coins[58, 8] = 1;
            coins[60, 3] = 1;
            coins[60, 4] = 1;
            coins[60, 5] = 1;
            coins[64, 8] = 1;
            coins[66, 5] = 1;
            coins[69, 4] = 1;
            coins[69, 5] = 1;
            coins[69, 6] = 1;
            coins[75, 1] = 1;
            coins[75, 2] = 1;
            coins[78, 6] = 1;
            coins[78, 7] = 1;
            coins[81, 8] = 1;
            coins[81, 8] = 1;
        }

        
        public void SetValues(Texture2D player_texture, Texture2D block_texture, Texture2D teacher1_texture, Texture2D teacher2_texture, Texture2D coin_texture, Texture2D hat_texture, Texture2D bomb_texture, SpriteBatch spriteBatch)
        {
            _Player = new Player(player_texture, new Vector2(448, ground_level - player_texture.Height), spriteBatch);
            _Board = new Board(spriteBatch, block_texture, 87, 10);
            this.CoinsCoordinates();
            for (int i = 0; i < 87; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (coins[i, j] == 1)
                        _Coins.Add(new Coin(coin_texture, new Vector2(i * 64, j * 64), spriteBatch));
                }
            }

            _Teachers.Add(new Teacher(teacher1_texture, new Vector2(3840, ground_level - teacher1_texture.Height), spriteBatch));
            _Teachers.Add(new Teacher(teacher2_texture, new Vector2(3200, ground_level - teacher2_texture.Height), spriteBatch));
            _Teachers.Add(new Teacher(teacher2_texture, new Vector2(4672, ground_level - teacher2_texture.Height - 192), spriteBatch));
            _Hat = new Hat(hat_texture, new Vector2(85*64, ground_level - hat_texture.Height), spriteBatch);
            _Bomb = new BombQuestion(bomb_texture, new Vector2()
                /*new Vector2(random.Next((int)(Player.Sprite_vector.X - 320), (int)(Player.Sprite_vector.X + 320)), 0)*/, spriteBatch);
            
            Player.Score = 0;
            
        }
        public void Collisisons(GameTime gametime)
        {
            foreach (var item in Teachers)
            {
                item.DeleteTeacherEvent += e => Teachers.Remove(e);
                if (item.Collision(Player, gametime))
                {
                    this.Start(item, gametime);
                    break;
                }
                
            }
            foreach (var item in Coins)
            {
                item.DeleteCoinEvent += e => Coins.Remove(e);
                if(item.Collision(Player, gametime)) break;
            }
            foreach (var item in Bombs)
            {
                item.DeleteBombEvent += e => Bombs.Remove(e);
                if (item.Collision(Player, gametime)) break;
            }
        }
        
    public void Start(Teacher teacher, GameTime gametime)
        {
            while(Bombs.Count <10)
                Bombs.Add(Bomb);

            foreach (var bomb in Bombs)
            {
                bomb.Update(gametime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont Font, GameTime gameTime)
        {
            Player.Draw();
            Board.Draw();
            for (int i = 0; i < Teachers.Count(); i++)
            {
                Teachers[i].Draw();
            }
            foreach (var item in _Coins)
            {
                item.Draw();
            }
            foreach (var item in Bombs)
            {
                item.Draw();
            }
            
            Hat.Draw();
            spriteBatch.DrawString(Font, Convert.ToString("NAKOPLENNAYA: " + 0.2 * Player.Score), new Vector2(Player.Sprite_vector.X - 30, Player.Sprite_vector.Y - 30), Color.Azure);//new Vector2(Player.Sprite_vector.X - 7*64, 64), Color.Azure);           
            spriteBatch.DrawString(Font, Convert.ToString("Time: " + Math.Floor(timer += gameTime.ElapsedGameTime.TotalSeconds)), new Vector2(Player.Sprite_vector.X - 12, Player.Sprite_vector.Y - 60), Color.Azure);
        }
    }
}
