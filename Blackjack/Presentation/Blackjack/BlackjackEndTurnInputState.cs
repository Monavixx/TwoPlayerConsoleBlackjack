using Blackjack.Application.Commands;
using ConsoleGameFramework.Input.InputStates;

namespace Blackjack.Presentation.Blackjack;

public class BlackjackEndTurnInputState : InputState
{
    public BlackjackEndTurnInputState(Domain.Blackjack blackjack)
    {
        RegisterCommand(ConsoleKey.S, () => new FinishTurnCommand(blackjack));
    }
}