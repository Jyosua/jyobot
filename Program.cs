﻿using System;

namespace jyobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = JsonHelper.Deserialize<Configuration>("configuration.json");
            var quotes = new QuoteManager("quotes.json").Initialize();

            var client = JyobotClient.Initialize(quotes, config);
            Console.ReadLine();
        }
    }
}
