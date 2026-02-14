namespace Blackjack.Domain;

public record struct Card(CardValue Value, Suit Suit)
{
    public override string ToString()
        => $"{Value} of {Suit}";
}