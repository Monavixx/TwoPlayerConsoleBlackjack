using Blackjack.Application.Commands;
using ConsoleGameFramework.Input.InputStates;

namespace Blackjack.Presentation.Blackjack;

public class BlackjackTurnInputState : InputState
{
    public BlackjackTurnInputState(Domain.Blackjack blackjack)
    {
        RegisterCommand(ConsoleKey.T, () => new TakeNewCardCommand(blackjack));
        RegisterCommand(ConsoleKey.S, () => new FinishTurnCommand(blackjack));
    }
}