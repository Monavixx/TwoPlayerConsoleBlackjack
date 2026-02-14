using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;

namespace Blackjack.Application.Commands;

public class FinishTurnCommand (Domain.Blackjack blackjack): ICommand
{
    public InputHandleResult? Execute()
    {
        blackjack.Stand();
        return InputHandleResult.None();
    }
}