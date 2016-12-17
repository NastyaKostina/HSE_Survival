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

namespace ScoresForm
{
    public partial class EnterYourNameForm : Form
    {
        SqlConnection sqlConnection = new SqlConnection("Data Source=tcp:hseserver.database.windows.net,1433;Initial Catalog = PlayerScoresDatabase; User ID = vvsdobnov; Password=03041997Gaga");
        SqlCommand sqlCommand;
        public int scores;
        public int time;

        public EnterYourNameForm(int score, int time)
        {
            InitializeComponent();
            listView1.View = View.Details;
            label5.Text = Convert.ToString(score); label4.Text = Convert.ToString(time);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("INSERT INTO PlayerScoreTable (Id,Name,Score,Time) values('" + 5 + "','" + textBox1.Text + "','" + Convert.ToInt32(label4.Text) + "','" + Convert.ToInt32(label5.Text) + "')", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Ваш результат внесен в таблицу рекордов!");
                listView1.Items.Clear();
                button1.Enabled = false;
                string sqlCommandText = "SELECT * FROM PlayerScoreTable";
                SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText, sqlConnection);

                using (SqlDataReader reader = sqlCommand1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem A = new ListViewItem();
                        ListViewItem.ListViewSubItem B = new ListViewItem.ListViewSubItem();
                        ListViewItem.ListViewSubItem C = new ListViewItem.ListViewSubItem();
                        A.Text = reader.GetString(1);
                        B.Text = Convert.ToString(reader.GetInt32(2));
                        C.Text = Convert.ToString(reader.GetInt32(3));
                        A.SubItems.Add(B); A.SubItems.Add(C);
                        listView1.Items.Add(A);
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                sqlConnection.Close();
            }
        }

        private void EnterYourNameForm_Load(object sender, EventArgs e)
        {
            sqlConnection.Open();
            string sqlCommandText = "SELECT * FROM PlayerScoreTable";
            SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText, sqlConnection);

            using (SqlDataReader reader = sqlCommand1.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListViewItem A = new ListViewItem();
                    ListViewItem.ListViewSubItem B = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem C = new ListViewItem.ListViewSubItem();
                    A.Text = reader.GetString(1);
                    B.Text = Convert.ToString(reader.GetInt32(2));
                    C.Text = Convert.ToString(reader.GetInt32(3));
                    A.SubItems.Add(B); A.SubItems.Add(C);
                    listView1.Items.Add(A);
                }
            }
            sqlConnection.Close();
        }
    }
}
