using Blackjack.Domain;

namespace UnitTests;

public class FakeCardDeckGenerator : ICardDeckGenerator
{
    private readonly Card[] _cards;
    private int _curPos = 0;
    public FakeCardDeckGenerator(Card[] cards)
    {
        _cards = cards;
    }
    public Card Next()
    {
        return _cards[_curPos++];
    }

    public void Reset()
    {
        _curPos = 0;
    }
}