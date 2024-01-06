public class LoadingStatePresenter : IStatePresenter {
    private readonly GameController _controller;
    private readonly IMenuCommand _playGameCommand;

    public LoadingStatePresenter(CommandFactory factory, GameController controller) {
        _controller = controller;
        _playGameCommand = factory.CreateRoute<GameScreenState>(controller.PlayGame);
    }
    
    public void EnterState(ScreenState state) {
        _controller.PrepareGame();
        Delay.Execute(2f, _playGameCommand.Execute);
    }
    
    public void ExitState(ScreenState state) {}
}