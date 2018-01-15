using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreWebApiSamples.Helpers
{
    public static class ApplicationBuilderExtensions
    {
        //public static IApplicationBuilder UseWebApiExceptionHandler(this IApplicationBuilder app)
        //{
        //    return app.UseExceptionHandler(WebApiExceptionHandler().Result);
        //}

        //private static async Task<Action<IApplicationBuilder>> WebApiExceptionHandler()
        //{
        //    return errorApp =>
        //    {
        //        errorApp.Run(async context =>
        //        {
        //            context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
        //            context.Response.ContentType = "application/json";

        //            var error = context.Features.Get<IExceptionHandlerFeature>();
        //            if (error != null)
        //            {
        //                var ex = error.Error;

        //                await context.Response.WriteAsync(new ErrorDto()
        //                {
        //                    Code = (int)HttpStatusCode.BadRequest,
        //                    Message = "An unexpected error has happened, please try again later or call your administrator."//ex.Message
        //                }.ToString(), Encoding.UTF8);
        //            }
        //        });
        //    };
        //}
    }
}