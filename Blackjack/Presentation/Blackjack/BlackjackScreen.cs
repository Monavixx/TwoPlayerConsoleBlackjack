using System.Diagnostics.CodeAnalysis;
using ConsoleGameFramework.Screens;
using ConsoleGameFramework.Viewport;

namespace Blackjack.Presentation.Blackjack;

public class BlackjackScreen : Screen
{
    private readonly Domain.Blackjack _blackjack;
    private readonly BlackjackRenderer _renderer;
    [SetsRequiredMembers]
    public BlackjackScreen(IViewport viewport, Domain.Blackjack blackjack) : base(viewport)
    {
        _blackjack = blackjack;
        _renderer = new BlackjackRenderer(blackjack);
        SetInputState(new BlackjackTurnInputState(blackjack));
        _blackjack.GameEnded += BlackjackOnGameEnded;
        _blackjack.GameStarted += BlackjackOnGameStarted;
    }

    private void BlackjackOnGameStarted()
    {
        SetInputState(new BlackjackTurnInputState(_blackjack));
    }

    private void BlackjackOnGameEnded()
    {
        SetInputState(new BlackjackEndGameInputState(_blackjack));
    }

    public override void Render()
    {
        _renderer.Render();
    }
}