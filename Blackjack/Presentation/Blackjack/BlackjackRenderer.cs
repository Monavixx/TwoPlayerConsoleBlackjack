using Blackjack.Domain;

namespace Blackjack.Presentation.Blackjack;

public sealed class BlackjackRenderer : IDisposable
{
    private bool _fullRenderRequired = true;
    private List<Card> _addedCards = [];
    private readonly Domain.Blackjack _blackjack;

    public BlackjackRenderer(Domain.Blackjack blackjack)
    {
        _blackjack = blackjack;
        _blackjack.GameStarted += BlackjackOnGameStarted;
        _blackjack.GameEnded += BlackjackOnGameEnded;
        _blackjack.NextStageStarted += BlackjackOnNextStageStarted;
        _blackjack.NextStageStarting += BlackjackOnNextStageStarting;
        _blackjack.State.CurrentPlayer.CardAdded += CurrentPlayerOnCardAdded;
    }
    public void Dispose()
    {
        _blackjack.GameStarted -= BlackjackOnGameStarted;
        _blackjack.GameEnded -= BlackjackOnGameEnded;
        _blackjack.NextStageStarted -= BlackjackOnNextStageStarted;
        _blackjack.NextStageStarting -= BlackjackOnNextStageStarting;
    }
    private void BlackjackOnNextStageStarting()
    {
        _blackjack.State.CurrentPlayer.CardAdded -= CurrentPlayerOnCardAdded;
    }
    private void CurrentPlayerOnCardAdded(Card card)
    {
        _addedCards.Add(card);
    }
    private void BlackjackOnNextStageStarted()
    {
        if (_blackjack.State.Stage is GameStage.FirstPlayersTurn or GameStage.SecondPlayersTurn)
        {
            _blackjack.State.CurrentPlayer.CardAdded += CurrentPlayerOnCardAdded;
        }
        _fullRenderRequired = true;
        _addedCards.Clear();
    }
    private void BlackjackOnGameEnded()
    {
        _fullRenderRequired = true;
        _addedCards.Clear();
    }

    private void BlackjackOnGameStarted()
    {
        _fullRenderRequired = true;
        _addedCards.Clear();
    }

    public void Render()
    {
        if (_fullRenderRequired)
        {
            Console.Clear();
            FullRender();
        }
        else
        {
            if (_addedCards.Count > 0)
            {
                RenderAddedCards();
                RenderScore();
            }
        }
        _fullRenderRequired = false;
    }

    private void FullRender()
    {
        Console.ResetColor();
        switch (_blackjack.State.Stage)
        {
            case GameStage.SecondPlayersTurn or GameStage.FirstPlayersTurn:
            {
                string tempToRender = $"{_blackjack.State.CurrentPlayer.Name}, it's your turn.";
                int maxLeftLength = tempToRender.Length;
                Console.WriteLine(tempToRender);

                var hand = _blackjack.State.CurrentPlayer.Hand;
                _cardsRendered = hand.Length;
                Console.WriteLine($"Your score: {_blackjack.BestPlayerHandValue(_blackjack.State.CurrentPlayer)}");
                Console.WriteLine("Your cards:");
                foreach (var card in hand)
                {
                    Console.WriteLine($"\t{card}");
                }

                Console.SetCursorPosition(maxLeftLength + 4, 2);
                Console.Write("Press T to take a new card");
                Console.SetCursorPosition(maxLeftLength + 4, 3);
                Console.Write("Press S to finish your turn");
                break;
            }
            case GameStage.Draw:
            {
                Console.WriteLine("Draw");
                goto finish;
            }
            case GameStage.FirstPlayerWon:
            {
                Console.WriteLine($"{_blackjack.State.FirstPlayer.Name} has won");
                goto finish;
            }
            case GameStage.SecondPlayerWon:
            {
                Console.WriteLine($"{_blackjack.State.SecondPlayer.Name} has won");
                goto finish;
            } finish:
            {
                string tempToRender = $"{_blackjack.State.FirstPlayer.Name}'s cards:";
                int maxLeftLength = tempToRender.Length;
                Console.WriteLine(tempToRender);
                var hand = _blackjack.State.FirstPlayer.Hand;
                foreach (var card in hand)
                {
                    tempToRender = $"  {card}";
                    maxLeftLength = int.Max(maxLeftLength, tempToRender.Length);
                    Console.WriteLine(tempToRender);
                }
                Console.Write("    "+_blackjack.BestPlayerHandValue(_blackjack.State.FirstPlayer));
                Console.SetCursorPosition(maxLeftLength+3, 1);
                Console.Write($"{_blackjack.State.SecondPlayer.Name}'s cards:");
                hand = _blackjack.State.SecondPlayer.Hand;
                for (var i = 0; i < hand.Length; i++)
                {
                    Console.SetCursorPosition(maxLeftLength + 5, 2+i);
                    Console.Write(hand[i]);
                }
                Console.SetCursorPosition(maxLeftLength + 8, 2+hand.Length);
                Console.Write(_blackjack.BestPlayerHandValue(_blackjack.State.SecondPlayer));

                break;
            }
        }
    }

    private int _cardsRendered = 0;
    private void RenderAddedCards()
    {
        for (int i = 0; i < _addedCards.Count; i++)
        {
            Console.SetCursorPosition(0, _cardsRendered++ + 3);
            Console.Write(_addedCards[i]);
        }
        _addedCards.Clear();
    }

    private void RenderScore()
    {
        Console.SetCursorPosition(12,1);
        Console.WriteLine(_blackjack.BestPlayerHandValue(_blackjack.State.CurrentPlayer));
    }
}