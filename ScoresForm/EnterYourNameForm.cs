using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using ConnectionToDatabase;

namespace ScoresForm
{
    public partial class EnterYourNameForm : Form
    {
        public int scores;
        public int time;
        Operations operations = new Operations();

        public EnterYourNameForm(int time, int score)
        {
            InitializeComponent();
            timer1.Start();
            listView1.View = View.Details;
            label4.Text = Convert.ToString(0.1 * score); label5.Text = Convert.ToString(time);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                operations.AddToDatabase(textBox1.Text, label4.Text, label5.Text);
                List<string> scores = operations.DisplayingDatabase();
                listView1.Items.Clear();
                for (int i = 0; i < scores.Count; i += 4)
                {
                    ListViewItem A = new ListViewItem();
                    ListViewItem.ListViewSubItem B = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem C = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem D = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem E = new ListViewItem.ListViewSubItem();
                    A.Text = scores[i];
                    B.Text = scores[i + 1];
                    C.Text = scores[i + 2];
                    D.Text = scores[i + 3];
                    E.Text = Convert.ToString(Math.Round((Convert.ToDouble(scores[i + 1]) / Convert.ToDouble(scores[i + 2])) * 10, 4));
                    A.SubItems.Add(B); A.SubItems.Add(C); A.SubItems.Add(D); A.SubItems.Add(E);
                    listView1.Items.Add(A);
                }
                timer1.Stop();
                button1.Enabled = false;
                textBox1.Enabled = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void EnterYourNameForm_Load(object sender, EventArgs e)
        {
            try
            {
                List<string> scores = operations.DisplayingDatabase();
                for (int i = 0; i < scores.Count; i += 4)
                {
                    ListViewItem A = new ListViewItem();
                    ListViewItem.ListViewSubItem B = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem C = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem D = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem E = new ListViewItem.ListViewSubItem();
                    A.Text = scores[i];
                    B.Text = scores[i + 1];
                    C.Text = scores[i + 2];
                    D.Text = scores[i + 3];
                    E.Text = Convert.ToString(Math.Round(Convert.ToDouble(scores[i + 1]) / Convert.ToDouble(scores[i + 2]), 2));
                    A.SubItems.Add(B); A.SubItems.Add(C); A.SubItems.Add(D); A.SubItems.Add(E);
                    listView1.Items.Add(A);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                button1.Enabled = false;
            }
            else { button1.Enabled = true; }
        }
    }
}
