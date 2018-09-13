using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicToeApi.Attributes;
using TicToeApi.DataAccess;
using TicToeApi.Model;

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
        [Log]
        public void  Register([FromBody]User user)
        {
            register.Register(user);
        }


        [HttpGet]
        [Log]
        public List<User> Get()
        {
            userList=getAllUser.GetAll();
            return userList;

        }


    }
}