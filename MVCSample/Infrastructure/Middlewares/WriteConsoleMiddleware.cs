using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Middlewares
{
    public class WriteConsoleMiddleware
    {
        public RequestDelegate Next { get; }
        public string Caption { get; }

        public WriteConsoleMiddleware(RequestDelegate next, string caption)
        {
            Next = next;
            Caption = caption;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"{Caption} before");
            await Next(context);
            Console.WriteLine($"{Caption} After");
        }
    }
}
