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
    [Route("api/Game")]
    public class GameController : Controller
    {
        static int[] box = new int[9];
        [HttpGet]
        public int[] GetStatus()
        {
            return box;
        }

        [HttpPut]
        [ValidToken]
        public void MakeAMove([FromBody]int id)
        {

            box[id-1] = id;
        }

    }
}