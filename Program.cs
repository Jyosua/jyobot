using System;
using Jyobot.Models;
using Jyobot.Workers;

namespace Jyobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = JsonHelper.Deserialize<Configuration>("configuration.json");
            var commands = JsonHelper.Deserialize<CommandCollection>("commands.json");
            var quotes = new QuoteManager("quotes.json").Initialize();

            var client = JyobotClient.Initialize(quotes, commands, config);
            Console.ReadLine();
        }
    }
}
