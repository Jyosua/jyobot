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
    }
}