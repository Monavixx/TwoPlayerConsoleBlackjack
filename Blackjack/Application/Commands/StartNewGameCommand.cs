using Blackjack.Presentation.Blackjack;
using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;

namespace Blackjack.Application.Commands;

public class StartNewGameCommand (Domain.Blackjack blackjack) : ICommand
{
    public InputHandleResult? Execute()
    {
        blackjack.StartNewGame(new BlackjackConfiguration(blackjack.State.FirstPlayer.Name, blackjack.State.SecondPlayer.Name));
        return InputHandleResult.NavigateTo<BlackjackScreen>();
    }
}