using System;
using System.Collections.Generic;

namespace jyobot
{
    public class QuoteCollection {
        public List<string> Quotes;
    }

    public class QuoteManager
    {
        string filename;
        QuoteCollection quotes;
        Random random = new Random();

        public QuoteManager(string filename) {
            this.filename = filename;
        }

        public QuoteManager Initialize()
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