public class PlayStatePresenter : IStatePresenter {
    
    private readonly GameController _controller;
    private readonly IMenuCommand _endGameCommand;

    public PlayStatePresenter(CommandFactory factory, GameController controller) {
        _controller = controller;
        _endGameCommand = factory.CreateRoute<CompleteScreenState>();
    }

    public void EnterState(ScreenState state) {
        _controller.GameEnd += _endGameCommand.Execute;
    }

    public void ExitState(ScreenState state) {
        _controller.GameEnd -= _endGameCommand.Execute;
    }
}
