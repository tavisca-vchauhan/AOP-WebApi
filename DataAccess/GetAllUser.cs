using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TicToeApi.DataAccess
{
    public class GetAllUser
    {
        public List<User> GetAll()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=TAVDESK013\SQLEXPRESS;Initial Catalog=game;User ID= sa; Password=test123!@#";
            con.Open();
            string query = "select * from game";
            List<User> userList = new List<User>();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    User user = new User();
                    user.Name = Convert.ToString(dataReader[1]);
                    user.UserName = Convert.ToString(dataReader[2]);
                    user.Token = Convert.ToString(dataReader[3]);
                    userList.Add(user);
                }
            }
            con.Close();
            return userList;
        }
    }
}
