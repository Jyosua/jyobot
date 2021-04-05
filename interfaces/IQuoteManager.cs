namespace Jyobot.Interfaces
{
    public interface IQuoteManager
    {
        IQuoteManager Initialize();
        void SaveQuotes();
        string GetQuote();
    }
}