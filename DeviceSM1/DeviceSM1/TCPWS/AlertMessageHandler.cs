using DeviceSM1.Data;
using DeviceSM1.Models.Table;
using DeviceSM1.SocketServer;
using DeviceSM1.TCPServer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSM1.TCPWS
{
    public class AlertMessageHandler : WebSocketHandler, TCPSocketHandler
    {
        private Server _server;
        private TCPDbContext _tcpDbContext;
        public AlertMessageHandler(WebSocketConnectionManager webSocketConnectionManager, Server server, TCPDbContext tcpDbContext) 
            : base(webSocketConnectionManager)
        {
            _server = server;
            _tcpDbContext = tcpDbContext;

            _server.Listen(Constant.TCP_SERVER_PORT);

            _server.OnClientConnect += OnClientConnected;
            _server.OnClientDisconnect += OnClientDisconnected;
            _server.OnReceiveData += OnReceivedData;
        }

        public void OnClientConnected(int clientNumber)
        {
        }

        public void OnClientDisconnected(int clientNumber)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            //await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public async void OnReceivedData(int clientNumber, byte[] message, int messageSize)
        {
            var data = Encoding.UTF8.GetString(message, 0, messageSize);

            Log packet = new Log();
            packet.ParseString(data);

            if (packet.IsValid)
            {
                await CheckThresholdAsync(packet);
            }
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            //await SendMessageToAllAsync(message);
        }

        private async Task CheckThresholdAsync(Log packet)
        {
            // Get device information from DB
            var device = _tcpDbContext.Devices
                .Where(d => d.IMEI == packet.IMEI)
                .Include(d => d.Sensors)
                    .ThenInclude(d => d.SensorType)
                .Include(d => d.User)
                .Include(d => d.Location)
                .FirstOrDefault();

            if (device == null)
                return;

            var sensor1 = device.Sensors
                .Where(d => d.SensorTypeId == 1)
                .FirstOrDefault();

            packet.Alerts = new List<Alert>();

            if (sensor1 != null)
            {
                if (packet.SensorValue1 < sensor1.LowThreshold || packet.SensorValue1 > sensor1.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor1
                    };
                    packet.Alerts.Add(alert);
                }
            }

            var sensor2 = device.Sensors
                .Where(d => d.SensorTypeId == 2)
                .FirstOrDefault();

            if (sensor2 != null)
            {
                if (packet.SensorValue2 < sensor2.LowThreshold || packet.SensorValue2 > sensor2.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor2
                    };
                    packet.Alerts.Add(alert);
                }
            }

            var sensor3 = device.Sensors
                .Where(d => d.SensorTypeId == 3)
                .FirstOrDefault();

            if (sensor3 != null)
            {
                if (packet.SensorValue3 < sensor3.LowThreshold || packet.SensorValue3 > sensor3.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor3
                    };
                    packet.Alerts.Add(alert);
                }
            }

            var sensor4 = device.Sensors
                .Where(d => d.SensorTypeId == 4)
                .FirstOrDefault();

            if (sensor4 != null)
            {
                if (packet.SensorValue4 < sensor4.LowThreshold || packet.SensorValue4 > sensor4.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor4
                    };
                    packet.Alerts.Add(alert);
                }
            }

            var sensor5 = device.Sensors
                .Where(d => d.SensorTypeId == 5)
                .FirstOrDefault();

            if (sensor5 != null)
            {
                if (packet.SensorValue5 < sensor5.LowThreshold || packet.SensorValue5 > sensor5.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor5
                    };
                    packet.Alerts.Add(alert);
                }
            }

            var sensor6 = device.Sensors
                .Where(d => d.SensorTypeId == 6)
                .FirstOrDefault();

            if (sensor2 != null)
            {
                if (packet.SensorValue6 < sensor6.LowThreshold || packet.SensorValue6 > sensor6.HighThreshold)
                {
                    Alert alert = new Alert
                    {
                        Sensor = sensor6
                    };
                    packet.Alerts.Add(alert);
                }
            }

            if (packet.Alerts.Count > 0)
            {
                _tcpDbContext.Logs.Add(packet);
                await _tcpDbContext.SaveChangesAsync();

                string alertMessage = JsonConvert.SerializeObject(packet,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        DateFormatString = "M/d/yyyy h:mm:ss tt"
                    });

                await SendMessageToAllAsync(alertMessage);
            }
        }
    }
}
