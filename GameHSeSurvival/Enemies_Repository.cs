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
        #region Teacher
        public List<Teacher> Teachers = new List<Teacher>();
        public void DrawTeacher(SpriteBatch sb)
        {
            for (int i = 0; i < Teachers.Count(); i++)
            {
                Teachers[i].Draw();
            }
        }
        public void Collisions(Player player)
        {
            for (int i = 0; i < Teachers.Count(); i++)
            {
                if  (Teachers[i].HurtOrKilledBy(player)[0])
                {
                    Teachers.Remove(Teachers[i]);
                    i--;
                    player.move -= Vector2.UnitY * 25f;
                    break;
                }

                if (Teachers[i].HurtOrKilledBy(player)[1])
                {
                    player.Sprite_vector = new Vector2(550, 576 - player.Sprite_texture.Height);
                    break;
                }
                if (Teachers[i].HurtOrKilledBy(player)[0] == false && Teachers[i].HurtOrKilledBy(player)[1] == false)
                { }
            }
        }
        #endregion

        #region Coins
        int[,] coins = new int[87, 10];
        List<Coin> ListCoins = new List<Coin>();
        public void Method(Texture2D coin_texture, SpriteBatch sb)
        {
            #region CoordinatsOfCoins
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
            #endregion
            for (int i = 0; i < 87; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (coins[i,j] == 1)
                        ListCoins.Add(new Coin(coin_texture, new Vector2(i * 64, j * 64), sb));
                }
            }
        }
        public void Draw()
        {
            foreach (var item in ListCoins)
            {
                item.Draw();
            }
        }
        public void CollisionsCoins(Sprite obj)
        {
            for (int i = 0; i < ListCoins.Count; i ++)
            {
                if (obj.rectangle.Intersects(ListCoins[i].rectangle))
                    ListCoins.RemoveAt(i);
            }
        }
        #endregion

    }
}
