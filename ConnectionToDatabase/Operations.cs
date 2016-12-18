using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ConnectionToDatabase
{
    public class Operations
    {
        string sqlConnectionString = ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString;
        string sqlCommandText = "SELECT * FROM PlayerScoreTable";
        SqlCommand sqlCommand;

        public void AddToDatabase(string name, string score, string time)
        {
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
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
            sqlCommand.Parameters.Add(new SqlParameter("@Name", name));
            sqlCommand.Parameters.Add(new SqlParameter("@Score", Math.Round(Convert.ToDouble(score))));
            sqlCommand.Parameters.Add(new SqlParameter("@Time", Convert.ToInt32(time)));
            sqlCommand.Parameters.Add(new SqlParameter("@GamePlayed", DateTime.Now));
            sqlCommand.ExecuteNonQuery();
            SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText, sqlConnection);
            sqlConnection.Close();
        }

        public List<string> DisplayingDatabase()
        {
            List<string> scores = new List<string>();
            SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand1 = new SqlCommand(sqlCommandText, sqlConnection);
            using (SqlDataReader reader = sqlCommand1.ExecuteReader())
            {
                while (reader.Read())
                {
                    scores.Add(reader.GetString(1));
                    scores.Add(Convert.ToString(reader.GetInt32(2)));
                    scores.Add(Convert.ToString(reader.GetInt32(3)));
                    scores.Add(Convert.ToString(reader.GetDateTime(4)));
                }
            }
            sqlConnection.Close();
            return scores;
        }
    }
}
