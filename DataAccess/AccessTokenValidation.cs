using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicToeApi.DataAccess
{
    public class AccessTokenValidation
    {
        static int userNumber = 0;
        string key = "";
        static string firstToken = "";
        static string secondToken = "";
        public void ValidateAccessToken(string apiKey)
        {
            int count = 0;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=TAVDESK013\SQLEXPRESS;Initial Catalog=game;User ID= sa; Password=test123!@#";
            con.Open();
            string query = "select * from game";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            string apikey = Convert.ToString(apiKey);
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    key = Convert.ToString(dataReader[3]);
                    key = key.Trim();
                    if (key.Equals(apikey))
                    {
                        count++;
                        break;
                    }
                }
            }
            con.Close();
            if (userNumber < 2)
            {
                userNumber++;
            }
            else if (!key.Equals(firstToken) && !key.Equals(secondToken))
            {
                throw new UnauthorizedAccessException("Only Two Players Can Play");
            }
            if (userNumber == 1)
            {
                firstToken = apiKey;
            }
            else if (!apiKey.Equals(firstToken))
            {
                secondToken = apiKey;
            }
            if (count == 0)
                throw new UnauthorizedAccessException("Invalid Access-Token passed");

        }
    }
}
