using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool Player_Left;
        bool Player_Right;
        bool Player_Jump = false;
        int Force = 0;
        int Gravitation = 20;
        int jump_speed = 3;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Player_Left = true;
                    break;
                case Keys.Right:
                    Player_Right = true;
                    break;
                case Keys.Space:
                    if (!Player_Jump && IsOutOfGame(Player))
                    {
                        Player_Jump = true;
                        Force = Gravitation;
                        Player.Top -= jump_speed;
                    }
                    break;
                case Keys.Escape:
                    Close();
                    break;

            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Player_Left = false;
                    break;
                case Keys.Right:
                    Player_Right = false;
                    break;
            }
        }

        public bool IsOutOfGame(PictureBox Player1)
        {
            if (Player1.Location.X < 0 || Player1.Location.X > Sky.Width)
                return true; //out of width of the screen
            if (Player1.Location.Y + Player1.Height > Sky.Height - 3 || Player1.Location.Y < 0)
                return true; //out of top of the screen
            return false;
        }

        //move right left
        private void timer_move_Tick(object sender, EventArgs e)
        {
            if (Player.Right > Block.Left
                && Player.Left < Block.Right - Player.Width
                && Player.Bottom > Block.Top)
                Player_Right = false;

            if (Player.Left < Block.Right && Player.Right > Block.Left + Player.Width / 2
                && Player.Bottom > Player.Top)
                Player_Left = false;
            Student.Movements(Player_Right, Player_Left, this);
            
        }

        
        //jump
        private void timer_Jump_Tick(object sender, EventArgs e)
        {
            if (Force > 0)
            {
                if (IsOutOfGame(Player)) Force = 0;
                else
                {
                    Force--;
                    Player.Top -= jump_speed;
                }
            }
            else
                Player_Jump = false;

            if (!Player_Jump && Player.Location.Y + Player.Height < Sky.Height - 3
                && !IsOutOfGame(Player))
                Player.Top += jump_speed;

            if (!Player_Jump && Player.Location.Y + Player.Height > Sky.Height - 1)
                Player.Top--;
        }
    
    }
}
