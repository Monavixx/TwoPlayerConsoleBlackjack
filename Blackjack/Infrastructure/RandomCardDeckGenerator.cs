using Blackjack.Domain;

namespace Blackjack.Infrastructure;

public class RandomCardDeckGenerator : ICardDeckGenerator
{
    private readonly Random _random;
    private Stack<Card> _cards;

    public RandomCardDeckGenerator(Random? random = null)
    {
        _random = random ?? Random.Shared;
        Reset();
    }

    public Card Next()
        => _cards.Pop();

    public void Reset()
    {
        _cards = new(
            from cv in Enum.GetValues<CardValue>()
            from s in Enum.GetValues<Suit>()
            orderby _random.Next()
            select new Card(cv, s));
    }
}