using System;
using System.IO;
using Newtonsoft.Json;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace jyobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfig();

            ConnectionCredentials credentials = new ConnectionCredentials(config.Username, config.Token);
	        var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            
            var client = new TwitchClient(customClient);
            client.Initialize(credentials, config.Channel);

            client.OnJoinedChannel += Client_OnJoinedChannel;
            /*client.OnLog += Client_OnLog;
            client.OnJoinedChannel += Client_OnJoinedChannel;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnNewSubscriber += Client_OnNewSubscriber;
            client.OnConnected += Client_OnConnected;*/

            client.Connect();
            Console.ReadLine();
        }

        static void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Join reached");
            (sender as TwitchClient)?.SendMessage(e.Channel, "This is a test message.");
        }

        static Configuration GetConfig()
        {
            using var reader = new StreamReader("configuration.json");
            using var jsonReader = new JsonTextReader(reader);

            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<Configuration>(jsonReader);
        }
    }
}
