using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicToeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {

        [HttpPost]
        public string Register([FromBody]User user)
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
            return "You Have Successfully Created your account with AccessToken = "+token+"    NOTE: Please save this token for future use";
        }


        [HttpGet]
        public List<User> Get()
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