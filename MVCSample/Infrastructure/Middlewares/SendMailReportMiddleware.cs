using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MVCSample.Infrastructure.Services.Implementations;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Middlewares
{
    public class SendMailReportMiddleware
    {
        private IConfiguration _configuration;
        public IMessageService Message { get; }
        public RequestDelegate Next { get; }
        public string Caption { get; }

        public SendMailReportMiddleware(IConfiguration configuration, IMessageService message, RequestDelegate next, string caption)
        {
            _configuration = configuration;
            Message = message;
            Next = next;
            Caption = caption;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (Convert.ToBoolean(_configuration["MiddleWareSendReport:Active"]) 
                && context.Request.Path.Value.Equals("/Account/Register") 
                && context.Request.Method.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
            {
                Message.SendMessage( $"Host={context.Request.Host.Host};\r\n Params: {string.Join(',', context.Request.Query)};\r\n Date (UTC)={DateTime.UtcNow};\r\n Date (Local)={DateTime.Now}",
                    _configuration["MiddleWareSendReport:Email"], 
                    MessageType.Email);
            }
            //Console.WriteLine($"{Caption} before");
            await Next(context);
            //Console.WriteLine($"{Caption} After");
        }
    }
}
