using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    interface IRepository : IRepositoryGeneral
    {
        List<Teacher> Teachers { get;}
        List<Coin> Coins { get;}
        Player Player { get;}
        Board Board { get;}
        Hat Hat { get; }
        void SetValues(Texture2D player_texture, Texture2D block_texture, Texture2D teacher1_texture, Texture2D teacher2_texture, Texture2D coin_texture, Texture2D hat_texture, SpriteBatch spriteBatch);
        
        }
}
