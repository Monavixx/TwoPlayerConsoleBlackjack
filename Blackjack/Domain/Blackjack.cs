namespace Blackjack.Domain;

public class Blackjack
{
    private readonly ICardDeckGenerator _cardDeckGenerator;
    private BlackjackState? _state = null;

    public BlackjackState State
    {
        get => _state ?? throw new InvalidOperationException("State is null");
        private set => _state = value;
    }

    public event Action? GameStarted;
    public Player OtherPlayer => State.CurrentPlayer == State.FirstPlayer ? State.SecondPlayer : State.FirstPlayer;

    public event Action? GameEnded;
    public event Action? NextStageStarted;
    public event Action? NextStageStarting;
    // public event Action? CardAdded;

    private void OnGameEnded()
    {
        GameEnded?.Invoke();
    }
    private void OnGameStarted()
    {
        GameStarted?.Invoke();
    }
    private void OnNextStageStarted()
    {
        NextStageStarted?.Invoke();
    }
    private void OnNextStageStarting()
    {
        NextStageStarting?.Invoke();
    }
    public Blackjack(ICardDeckGenerator cardDeckGenerator)
    {
        _cardDeckGenerator = cardDeckGenerator;
    }

    public void StartNewGame(IBlackjackConfiguration config)
    {
        State = new BlackjackState(config.FirstPlayerName, config.SecondPlayerName);
        OnGameStarted();
    }

    public void Take()
    {
        State.CurrentPlayer.AddCard(_cardDeckGenerator.Next());
        var hand = State.CurrentPlayer.Hand;
        int numberOfAces = hand.Count(c => c.Value == CardValue.Ace);
        for (int i = 0; i < numberOfAces + 1; i++)
        {
            int handValue = hand.Aggregate((total: 0, acesUsed: 0), (total, card) =>
                card.Value == CardValue.Ace
                    ? (total.total + (total.acesUsed >= i ? 1 : 11), total.acesUsed + 1)
                    : ((int)card.Value + total.total, total.acesUsed)).total;
            if (handValue <= 21)
            {
                if (handValue == 21)
                {
                    NextStage();
                }

                return;
            }
        }

        State.Stage = State.Stage switch
        {
            GameStage.FirstPlayersTurn => GameStage.SecondPlayerWon,
            GameStage.SecondPlayersTurn => GameStage.FirstPlayerWon,
            _ => throw new ArgumentOutOfRangeException()
        };
        OnGameEnded();
    }

    public void Stand()
    {
        NextStage();
    }

    private void NextStage()
    {
        if (State.CurrentPlayer == State.FirstPlayer)
        {
            OnNextStageStarting();
            State.CurrentPlayer = State.SecondPlayer;
            State.Stage = GameStage.SecondPlayersTurn;
            OnNextStageStarted();
        }
        else if (State.CurrentPlayer == State.SecondPlayer)
        {
            OnNextStageStarting();
            FinishGame();
            OnNextStageStarted();
        }
    }

    private void FinishGame()
    {
        int firstPlayerValue = BestPlayerHandValue(State.FirstPlayer);
        int secondPlayerValue = BestPlayerHandValue(State.SecondPlayer);
        if (firstPlayerValue == secondPlayerValue)
        {
            State.Stage = GameStage.Draw;
        }
        else if (firstPlayerValue > secondPlayerValue)
        {
            State.Stage = GameStage.FirstPlayerWon;
        }
        else
        {
            State.Stage = GameStage.SecondPlayerWon;
        }
        OnGameEnded();
    }

    public int BestPlayerHandValue(Player player)
    {
        int bestValue = 0;
        var hand = player.Hand;
        int numberOfAces = hand.Count(c => c.Value == CardValue.Ace);
        int[] scores = new int[numberOfAces + 1];
        for (int i = 0; i < numberOfAces + 1; i++)
        {
            int handValue = hand.Aggregate((total: 0, acesUsed: 0), (total, card) =>
                card.Value == CardValue.Ace
                    ? (total.total + (total.acesUsed >= i ? 1 : 11), total.acesUsed + 1)
                    : ((int)card.Value + total.total, total.acesUsed)).total;
            
            if (handValue == 21) return 21;
            if (handValue < 21 && handValue > bestValue)
                bestValue = handValue;
            scores[i] = handValue;
        }

        return bestValue == 0 ? scores.Min() : bestValue;
    }
}