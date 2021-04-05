using System;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace jyobot
{
    public class JyobotClient : TwitchClient
    {
        QuoteManager QuoteManager;

        public JyobotClient(WebSocketClient clientOptions) : base(clientOptions) {}

        public static JyobotClient Initialize(QuoteManager quotes, Configuration config) {
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
            var client = (sender as JyobotClient) ?? throw new ArgumentException($"Casting error for sender in {nameof(OnMessageReceivedHandler)}");

            if (e.ChatMessage.Message.Equals("!quote"))
            {
                var quote = client.QuoteManager.GetQuote();
                if(string.IsNullOrEmpty(quote))
                    return;

                client.SendMessage(e.ChatMessage.Channel, quote);
            }
        }
    }
}