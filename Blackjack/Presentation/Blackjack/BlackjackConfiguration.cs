using Blackjack.Domain;

namespace Blackjack.Presentation.Blackjack;

public class BlackjackConfiguration : IBlackjackConfiguration
{
    public BlackjackConfiguration(string firstPlayerName, string secondPlayerName)
    {
        FirstPlayerName = firstPlayerName;
        SecondPlayerName = secondPlayerName;
    }

    public string FirstPlayerName { get; }
    public string SecondPlayerName { get; }
}