using Blackjack.Domain;
using Blackjack.Presentation.Blackjack;

namespace UnitTests;

public class BlackjackTests
{
    [Fact]
    public void Take_WhenSecondPlayersScoreGreaterThan21AndFirstPlayersScoreLessThan21_FirstPlayerShouldWin()
    {
        var fakeCardGenerator = new FakeCardDeckGenerator([
            new Card(CardValue.Ten, Suit.Spades),
            new Card(CardValue.Four, Suit.Diamonds),
            new Card(CardValue.Two, Suit.Hearts),
            new Card(CardValue.Ten, Suit.Hearts),
            new Card(CardValue.Two, Suit.Diamonds),
            new Card(CardValue.Ten, Suit.Diamonds)
        ]);
        var blackjack = new Blackjack.Domain.Blackjack(fakeCardGenerator);
        blackjack.StartNewGame(new BlackjackConfiguration("monavixx", "lofectr"));
        blackjack.Take();
        blackjack.Take();
        blackjack.Take();
        blackjack.Stand();
        blackjack.Take();
        blackjack.Take();
        blackjack.Take();
        Assert.Equal(GameStage.FirstPlayerWon, blackjack.State.Stage);
    }
}