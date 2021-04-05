using System;
using Jyobot.Interfaces;
using Jyobot.Models;
using Jyobot.Workers;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Jyobot
{
    public class JyobotClient : TwitchClient, IBotClient
    {
        public IQuoteManager QuoteManager { get; set; }

        public JyobotClient(WebSocketClient clientOptions) : base(clientOptions) {}

        public static JyobotClient Initialize(IQuoteManager quotes, Configuration config) {
            ConnectionCredentials credentials = new ConnectionCredentials(config.Username, config.Token);
	        var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            
            var client = new JyobotClient(customClient){ QuoteManager = quotes };
            client.Initialize(credentials, config.Channel);

            client.OnJoinedChannel += JyobotClient.OnJoinedChannelHandler;
            client.OnMessageReceived += JyobotClient.OnMessageReceivedHandler;

            client.Connect();

            return client;
        }

        static void OnJoinedChannelHandler(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"#{e.Channel} joined");
        }

        static void OnMessageReceivedHandler(object sender, OnMessageReceivedArgs e)
        {
            var client = (sender as IBotClient) ?? throw new ArgumentException($"Unexpected casting error for sender in {nameof(OnMessageReceivedHandler)}");

            CommandManager.RouteCommand(e.ChatMessage, client);
        }
    }
}