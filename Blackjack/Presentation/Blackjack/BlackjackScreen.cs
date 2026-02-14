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
        _renderer.Wait();
        _shouldWaitForTimerBeforeNextStage = true;
        SetInputState(new BlackjackEndGameInputState(_blackjack));
    }

    private TimeSpan _timerBeforeNextStage = TimeSpan.Zero;
    private bool _shouldWaitForTimerBeforeNextStage = false;
    public override void Update(TimeSpan deltaTime)
    {
        base.Update(deltaTime);
        if (_shouldWaitForTimerBeforeNextStage)
        {
            _timerBeforeNextStage += deltaTime;
            if (_timerBeforeNextStage > TimeSpan.FromSeconds(2))
            {
                _renderer.Continue();
                _shouldWaitForTimerBeforeNextStage = false;
                _timerBeforeNextStage = TimeSpan.Zero;
            }
        }
    }

    public override void Render()
    {
        _renderer.Render();
    }
}