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
    [Route("api/Game")]
    public class GameController : Controller
    {
        CheckBoxStatus checkBox = new CheckBoxStatus();
        static int[] box = new int[9];
        static int boxValue = 1;
        string winner = "";
        static string prekey = "";
        [HttpGet]
        [Log]
        public int[] GetStatus()
        {
            return box;
        }


        [HttpPut]
        [Log]
        [ValidToken]
        public string MakeAMove([FromBody]int id)
        {
            winner = checkBox.CheckBox(box);
            if (winner.Equals("Game Over : ") || winner.Equals("DRAW !"))
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = "Game Is Already Over !";
                return "Game Is Already Over !";
                throw new Exception("Game Is Already Over !");
            }
            if(id>9 && id<0)
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = "Invalid Data !";
                return "Invalid Data !";
                throw new Exception("Invalid Data !");
            }
            if(box[id-1]!=0)
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = "Box Id is already given. Please select another one !";
                return "Box Id is already given. Please select another one !";
                throw new Exception("Box Id is already given to other player !");
            }
            box[id-1] = (boxValue%2)+1;
            boxValue++;
            winner = checkBox.CheckBox(box);
            if(!winner.Equals("In Progress...") && !winner.Equals("DRAW !"))
            {
                winner += AccessTokenValidation.winnerName+"Won";
            }
            if (AccessTokenValidation.key.Equals(prekey))
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = "You Can't play two continous moves";
                throw new UnauthorizedAccessException("You Can't play two continous moves");
            }
            if (ValidTokenAttribute.requestType.Equals("PUT"))
            {
                prekey = AccessTokenValidation.key;
            }
            LogAttribute.status = winner;
            return winner;
        }

    }
}