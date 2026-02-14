namespace Blackjack.Domain;

public interface IBlackjackConfiguration
{
    string FirstPlayerName { get; }
    string SecondPlayerName { get; }
}