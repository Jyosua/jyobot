using System;
using Jyobot.Interfaces;
using Jyobot.Models;

namespace Jyobot
{

    public class QuoteManager : IQuoteManager
    {
        string filename;
        QuoteCollection quotes;
        Random random = new Random();

        public QuoteManager(string filename) {
            this.filename = filename;
        }

        public IQuoteManager Initialize()
        {
            quotes = JsonHelper.Deserialize<QuoteCollection>(filename);
            return this;
        }

        public void SaveQuotes()
        {
            JsonHelper.Serialize(quotes, filename);
        }

        public string GetQuote()
        {
            if((quotes?.Quotes?.Count ?? 0) < 1)
                return null;

            return quotes.Quotes[random.Next(0, quotes.Quotes.Count)];
        }
    }
}