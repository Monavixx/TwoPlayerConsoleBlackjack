

using Blackjack.Infrastructure;
using Blackjack.Presentation.Blackjack;
using Blackjack.Presentation.NewGameMenu;
using ConsoleGameFramework;
using ConsoleGameFramework.Screens.Menu;
using ConsoleGameFramework.Viewport;

var cardDeckGenerator = new RandomCardDeckGenerator();
var blackjack = new Blackjack.Domain.Blackjack(cardDeckGenerator);

var app = new Application(new ConsoleViewport());
app
    .RegisterScreens(sf =>
    {
        sf.Register<BlackjackScreen>(v => new BlackjackScreen(v, blackjack));
        sf.Register<NewGameScreen>(v => new NewGameScreen(v, app, blackjack));
    })
    .CurrentScreen<NewGameScreen>();
app.Run();