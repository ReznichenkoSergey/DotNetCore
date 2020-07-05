using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebSockets.Models.Classes;

namespace WebSockets.Controllers
{
    public class StreamController : Controller
    {
        public WebSocketsHandler _handler;

        public StreamController(WebSocketsHandler handler)
        {
            _handler = handler;
        }

        public async Task Get()
        {
            var context = ControllerContext.HttpContext;
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                //await _handler.HandleAsync(Guid.NewGuid(), webSocket);
                await _handler.HandleAsync(webSocket);
                //await SendMessage(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task SendMessage(WebSocket socket)
        {
            while (true)
            {
                byte[] arr = Encoding.ASCII.GetBytes("Hello!");
                await socket.SendAsync(new ArraySegment<byte>(arr), WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(2000);
            }
        }
    }
}
