using System.Diagnostics.CodeAnalysis;
using Blackjack.Presentation.Blackjack;
using ConsoleGameFramework.Screens;
using ConsoleGameFramework.Viewport;

namespace Blackjack.Presentation.NewGameMenu;

public class NewGameScreen : Screen
{
    private readonly ConsoleGameFramework.Application _app;
    private readonly Domain.Blackjack _blackjack;

    [SetsRequiredMembers]
    public NewGameScreen(IViewport viewport, ConsoleGameFramework.Application app, Domain.Blackjack blackjack) : base(viewport)
    {
        _app = app;
        _blackjack = blackjack;
    }

    public override void Render()
    {
        Console.WriteLine("Welcome to blackjack!");
        Console.Write("First player name: ");
        string firstPlayerName = Console.ReadLine() ?? "";
        Console.Write("Second player name: ");
        string secondPlayerName = Console.ReadLine() ?? "";
        var conf = new BlackjackConfiguration(firstPlayerName, secondPlayerName);
        _blackjack.StartNewGame(conf);
        _app.CurrentScreen<BlackjackScreen>();
    }
}