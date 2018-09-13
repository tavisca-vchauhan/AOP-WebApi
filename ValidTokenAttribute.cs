using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicToeApi.DataAccess;

namespace TicToeApi
{
    public class ValidTokenAttribute : ResultFilterAttribute, IActionFilter
    {
        AccessTokenValidation checkToken = new AccessTokenValidation();
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["key"].ToString();
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new UnauthorizedAccessException("Access-Token not passed");
            }
            else
            {
                checkToken.ValidateAccessToken(apiKey);
            }
        }
    }
}
