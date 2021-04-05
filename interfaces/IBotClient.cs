using TwitchLib.Client.Interfaces;

namespace Jyobot.Interfaces {
    public interface IBotClient : ITwitchClient
    {
        IQuoteManager QuoteManager { get; set; }
    }
}