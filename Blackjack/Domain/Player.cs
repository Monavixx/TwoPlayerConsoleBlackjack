using System.Collections.Immutable;

namespace Blackjack.Domain;

public sealed class Player
{
    private List<Card> _hand = [];
    public ImmutableArray<Card> Hand => _hand.ToImmutableArray();
    public string Name { get; }
    public event Action<Card>? CardAdded;

    public Player(string name)
    {
        Name = name;
    }

    public void AddCard(Card card)
    {
        _hand.Add(card);
        CardAdded?.Invoke(card);
    }
}