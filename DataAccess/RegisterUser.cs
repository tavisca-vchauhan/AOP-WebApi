using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicToeApi.Attributes;
using TicToeApi.Model;

namespace TicToeApi.DataAccess
{
    public class RegisterUser
    {
        [Log]
        public string Register(User user)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=TAVDESK013\SQLEXPRESS;Initial Catalog=game;User ID= sa; Password=test123!@#";
                con.Open();
                string query = "insert into game(Name,UserName,Token) values(@Name,@UserName,@Token)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                string token = Token();
                cmd.Parameters.AddWithValue("@Token", token);
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return "You Have Successfully Created your account with AccessToken = " + token + "    NOTE: Please save this token for future use";
            }
            catch (Exception e)
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = e.Message;
                throw new Exception(e.Message);
            }
        }

        string Token()
        {
            Guid g = Guid.NewGuid();
            string token = Convert.ToBase64String(g.ToByteArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            return token;
        }
    }
}
