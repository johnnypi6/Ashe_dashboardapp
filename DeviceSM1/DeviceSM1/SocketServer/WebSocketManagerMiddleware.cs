using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace DeviceSM1.SocketServer
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _webSocketHandler { get; set; }

        public WebSocketManagerMiddleware(RequestDelegate next,
                                         WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        public WebSocketManagerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next.Invoke(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            await _webSocketHandler.OnConnected(socket);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                    return;
                }

                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocketHandler.OnDisconnected(socket);
                    return;
                }

            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class WebSocketManagerMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebSocketManagerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WebSocketManagerMiddleware>();
        }
    }
}
