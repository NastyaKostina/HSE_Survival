using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Student
    {
        //movements right left
        static private int movement_speed = 5;

        static public void Movements(bool Player_Right, bool Player_Left, Form1 frm)
        {
            if (Player_Right && frm.Player.Right <= frm.Sky.Width - 3)
            {
                frm.Player.Left += movement_speed;
            }
            if (Player_Left && frm.Player.Left >= 3)
            {
                frm.Player.Left -= movement_speed;
            }
        }
        
    }
}
