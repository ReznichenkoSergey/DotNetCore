using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebSockets.Models.Classes
{
    public class WebSocketsHandler
    {
        public ConcurrentDictionary<string, WebSocket> websocketConnections = new ConcurrentDictionary<string, WebSocket>();
        
        public async Task HandleAsync(WebSocket webSocket)
        {
            //await SendMessageToSockets($"User with id <b>{connectionId}</b> has joined the chat");
            while (webSocket.State == WebSocketState.Open)
            {
                var message = await ReceiveMessage(webSocket);
                if (message != null)
                    await SendMessageToSockets(message);
            }
        }

        private async Task<string> ReceiveMessage(WebSocket webSocket)
        {
            var arraySegment = new ArraySegment<byte>(new byte[4096]);
            var receivedMessage = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);
            if (receivedMessage.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.Default.GetString(arraySegment).TrimEnd('\0');
                var content = JsonConvert.DeserializeObject<MessageInputInfo>(message);
                if(!content.Registered)
                {
                    if (!websocketConnections.ContainsKey(content.Login))
                    {
                        websocketConnections.TryAdd(content.Login, webSocket);
                        return $"<i>\"{content.Login}\" has joined the chat!</i>";
                    }
                }
                else
                {
                    return $"<b>{content.Login}</b>: {content.Content}";
                }
                /*if (message.StartsWith("Registration:"))
                {
                    var login = message.Substring("Registration:".Length);
                    websocketConnections.TryAdd(_login, webSocket);
                    return $"{_login} has joined the chat!";
                }
                else if (!string.IsNullOrWhiteSpace(message))
                {
                    if (message.StartsWith("Login"))
                    {
                        var login = message.Substring("Registration:".Length);
                        return $"<b>{_login}</b>: {message}";
                    }
                }*/
            }
            return null;
        }

        private async Task SendMessageToSockets(string message)
        {
            var arraySegment = new ArraySegment<byte>(Encoding.Default.GetBytes(message));
            foreach (var item in websocketConnections)
            {
                await item.Value.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

    }
}
