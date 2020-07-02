using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSocketsEx.Services;

namespace WebSocketsEx.Controllers
{
    public class StreamController : Controller
    {
        static int num = 0;
        public WebSocketsHandler _handler;

        public StreamController(WebSocketsHandler handler)
        {
            _handler = handler;
        }

        public async Task Get()
        {
            var context = ControllerContext.HttpContext;
            var isWebSocketRequest = context.WebSockets.IsWebSocketRequest;

            if(isWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                //await SendMessage(webSocket);
                await _handler.HandleAsync(Guid.NewGuid(), webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task SendMessage(WebSocket webSocket)
        {
            while(true)
            {
                var bytes = Encoding.ASCII.GetBytes($"{num++}.Message");
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(1000);
            }
        }

    }
}
