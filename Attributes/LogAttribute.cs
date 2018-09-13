using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TicToeApi.Attributes
{
    public class LogAttribute : ResultFilterAttribute, IActionFilter
    {
        public static string url = "";
        public static string request = "";
        public static string response = "";
        public static string status = "-";
        public static string exception = "-";
        private static Object _lock = typeof(LogAttribute);

        public void OnActionExecuted(ActionExecutedContext context)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=TAVDESK013\SQLEXPRESS;Initial Catalog=game;User ID= sa; Password=test123!@#";
            con.Open();
            string query = "insert into Log(url,request,response,status,exception) values(@url,@request,@response,@status,@exception)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@url", url);
            cmd.Parameters.AddWithValue("@request", request);
            cmd.Parameters.AddWithValue("@response", response);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@exception", exception);
            int i = cmd.ExecuteNonQuery();
            con.Close();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            url = context.HttpContext.Request.Host.Value + "" + context.HttpContext.Request.Path.Value;
            request = context.ActionDescriptor.DisplayName;
            if (context.HttpContext.Response.Body.CanWrite)
            {
                response = "Success";
            }
            else
            {
                response = "Failure";
            }
        }
    }
}