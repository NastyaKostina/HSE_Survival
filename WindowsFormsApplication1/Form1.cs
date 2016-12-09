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
        int movement_speed = 5;
        bool Player_Jump = false;
        int jump_speed = 3;
        int Force = 0;
        int Gravity = 20;
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
                        Force = Gravity;
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

        private void timer_move_Tick(object sender, EventArgs e)
        {
            if (Player_Right && Player.Right <= Sky.Width - 3)
            {
                Player.Left += movement_speed;
            }
            if (Player_Left && Player.Left >= 3)
            {
                Player.Left -= movement_speed;
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
