using ConsoleGameFramework.Commands;
using ConsoleGameFramework.Input;

namespace Blackjack.Presentation.NewGameMenu;

public class OpenNewGameMenuCommand : ICommand
{
    public InputHandleResult? Execute()
    {
        return InputHandleResult.NavigateTo<NewGameScreen>();
    }
}