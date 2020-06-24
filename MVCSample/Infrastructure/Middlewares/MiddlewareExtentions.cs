using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Middlewares
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseWriteToConsole(this IApplicationBuilder app, string output) => app.UseMiddleware<SendMailReportMiddleware>(output);
    }
}
