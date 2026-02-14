namespace Blackjack.Domain;

public interface ICardDeckGenerator
{
    Card Next();
    void Reset();
}