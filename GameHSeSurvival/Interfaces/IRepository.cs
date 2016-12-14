using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHSeSurvival
{
    interface IRepository
    {
        List<Teacher> Teachers { get;}
        List<Coin> Coins { get;}
        Player Player { get;}
        Board Board { get;}
    }
}
