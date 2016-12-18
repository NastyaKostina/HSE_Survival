﻿using System;
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

namespace ScoresForm
{
    public partial class EnterYourNameForm : Form
    {

        string sqlConnectionString = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
        SqlCommand sqlCommand;
        public int scores;
        public int time;

        public EnterYourNameForm(int time, int score)
        {
            InitializeComponent();
            timer1.Start();
            listView1.View = View.Details;
            label4.Text = Convert.ToString(0.01 * score); label5.Text = Convert.ToString(time);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string sqlConnectionString = settings[0].ToString();
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            try
            {
                sqlConnection.Open();
                List<int> listOfIds = new List<int>();
                string sqlCommandText2 = "SELECT (Id) FROM PlayerScoreTable";
                SqlCommand sqlCommand2 = new SqlCommand(sqlCommandText2, sqlConnection);
                using (SqlDataReader reader = sqlCommand2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfIds.Add(Convert.ToInt32(reader.GetInt32(0)));
                    }
                }

                sqlCommand = new SqlCommand("INSERT INTO PlayerScoreTable (Id,Name,Score,Time,GamePlayed) values(@Id,@Name, @Score, @Time, @GamePlayed)", sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter("@Id", listOfIds.Max() + 1));
                sqlCommand.Parameters.Add(new SqlParameter("@Name", textBox1.Text));
                sqlCommand.Parameters.Add(new SqlParameter("@Score", Math.Round(Convert.ToDouble(label4.Text))));
                sqlCommand.Parameters.Add(new SqlParameter("@Time", Convert.ToInt32(label5.Text)));
                sqlCommand.Parameters.Add(new SqlParameter("@GamePlayed", DateTime.Now));

                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Ваш результат внесен в таблицу рекордов!");
                listView1.Items.Clear();
                button1.Enabled = false;
                textBox1.Enabled = false;
                string sqlCommandText = "SELECT * FROM PlayerScoreTable";
                SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText, sqlConnection);

                using (SqlDataReader reader = sqlCommand1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ListViewItem A = new ListViewItem();
                        ListViewItem.ListViewSubItem B = new ListViewItem.ListViewSubItem();
                        ListViewItem.ListViewSubItem C = new ListViewItem.ListViewSubItem();
                        ListViewItem.ListViewSubItem D = new ListViewItem.ListViewSubItem();
                        A.Text = reader.GetString(1);
                        B.Text = Convert.ToString(reader.GetInt32(2));
                        C.Text = Convert.ToString(reader.GetInt32(3));
                        D.Text = Convert.ToString(reader.GetDateTime(4));
                        A.SubItems.Add(B); A.SubItems.Add(C); A.SubItems.Add(D);
                        listView1.Items.Add(A);
                    }
                }
                sqlConnection.Close();
                timer1.Stop();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                sqlConnection.Close();
            }
        }

        private void EnterYourNameForm_Load(object sender, EventArgs e)
        {
            //string sqlConnectionString = settings[0].ToString();//ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            try
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
                        ListViewItem.ListViewSubItem D = new ListViewItem.ListViewSubItem();
                        A.Text = reader.GetString(1);
                        B.Text = Convert.ToString(reader.GetInt32(2));
                        C.Text = Convert.ToString(reader.GetInt32(3));
                        D.Text = Convert.ToString(reader.GetDateTime(4));
                        A.SubItems.Add(B); A.SubItems.Add(C); A.SubItems.Add(D);
                        listView1.Items.Add(A);
                    }
                }
                sqlConnection.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
