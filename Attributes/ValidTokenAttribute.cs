using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicToeApi.DataAccess;

namespace TicToeApi.Attributes
{
    public class ValidTokenAttribute : ResultFilterAttribute, IActionFilter
    {
        AccessTokenValidation checkToken = new AccessTokenValidation();
        public static string requestType = "";
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        [Log]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["key"].ToString();
            requestType = context.HttpContext.Request.Method;
            if (string.IsNullOrEmpty(apiKey))
            {
                LogAttribute.response = "Failue";
                LogAttribute.exception = "Access-Token not passed";
                throw new UnauthorizedAccessException("Access-Token not passed");
            }
            else
            { 
                checkToken.ValidateAccessToken(apiKey);
            }
        }
    }
}
