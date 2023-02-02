using Fire.DataAccess.Exceptions;
using Fire.UI.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace Fire.UI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    int statuscode = 0;
                    var errormessage = new List<string>();
                    object response = new object();
                    context.Response.ContentType = "application/json";
                    var exceptionhandler = context.Features.Get<IExceptionHandlerFeature>();
                    statuscode = exceptionhandler.Error switch
                    {
                        SideException => 401,
                        _ => 500
                    };
                    errormessage.Add(exceptionhandler.Error.Message);
                    response = "";
                    

                    context.Response.StatusCode = statuscode;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });
            });
        }
    }
}
