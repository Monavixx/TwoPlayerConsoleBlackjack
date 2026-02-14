namespace Blackjack.Domain;

public class BlackjackState
{
    public Player FirstPlayer { get; }
    public Player SecondPlayer { get; }
    public Player CurrentPlayer { get; set; }
    public GameStage Stage { get; set; }

    public BlackjackState(string firstPlayerName, string secondPlayerName)
    {
        FirstPlayer = new Player(firstPlayerName);
        SecondPlayer = new Player(secondPlayerName);
        CurrentPlayer = FirstPlayer;
    }
}