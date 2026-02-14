using Blackjack.Application.Commands;
using Blackjack.Presentation.NewGameMenu;
using ConsoleGameFramework.Input.InputStates;

namespace Blackjack.Presentation.Blackjack;

public class BlackjackEndGameInputState : InputState
{
    public BlackjackEndGameInputState(Domain.Blackjack blackjack)
    {
        RegisterCommand(ConsoleKey.Enter, () => new StartNewGameCommand(blackjack));
        RegisterCommand(ConsoleKey.Z, () => new OpenNewGameMenuCommand());
    }
}