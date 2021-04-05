using System;
using Jyobot.Interfaces;
using TwitchLib.Client.Models;

namespace Jyobot.Workers
{
    public static class CommandManager
    {
        public static void RouteCommand(ChatMessage message, IBotClient client)
        {
            LogCommand(message);

            switch(message?.Message)
            {
                case "!quote":
                    HandleQuote(message?.Channel, client);
                    break;
                default:
                    HandleStandardCommand(message, client);
                    break;
            }
        }

        public static void LogCommand(ChatMessage message)
        {
            if(message?.Message?.StartsWith("!") == true)
                Console.WriteLine($"{message.Username} invoked command {message.Message}");
        }

        public static void HandleQuote(string channel, IBotClient client)
        {
            if(string.IsNullOrEmpty(channel))
                return;

            var quote = client?.QuoteManager?.GetQuote();
            if(string.IsNullOrEmpty(quote))
                return;

            client.SendMessage(channel, quote);
        }

        public static void HandleStandardCommand(ChatMessage message, IBotClient client)
        {
            if(string.IsNullOrEmpty(message?.Channel))
                return;

            var command = message?.Message?.TrimStart('!');
            if(command == message?.Message)
                return;

            if(client?.StandardCommands?.ContainsKey(command) == true)
                client.SendMessage(message?.Channel, client.StandardCommands[command]);
        }
    }
}