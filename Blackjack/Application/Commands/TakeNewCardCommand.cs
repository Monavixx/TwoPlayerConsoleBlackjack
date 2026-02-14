using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;

namespace Blackjack.Application.Commands;

public class TakeNewCardCommand (Domain.Blackjack blackjack) : ICommand
{
    public InputHandleResult? Execute()
    {
        blackjack.Take();
        return InputHandleResult.None();
    }
}