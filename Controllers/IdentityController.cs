using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicToeApi.DataAccess;

namespace TicToeApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        RegisterUser register = new RegisterUser();
        GetAllUser getAllUser = new GetAllUser();
        List<User> userList = new List<User>();

        [HttpPost]
        public void  Register([FromBody]User user)
        {
            register.Register(user);
        }


        [HttpGet]
        public List<User> Get()
        {
            userList=getAllUser.GetAll();
            return userList;

        }


    }
}