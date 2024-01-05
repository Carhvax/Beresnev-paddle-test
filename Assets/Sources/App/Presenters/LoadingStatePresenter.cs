public class LoadingStatePresenter : IStatePresenter {
    private readonly IMenuCommand _playGameCommand;

    public LoadingStatePresenter(CommandFactory factory) {
        _playGameCommand = factory.CreateRoute<GameScreenState>();
    }
    
    public void EnterState(ScreenState state) {
        Delay.Execute(2f, _playGameCommand.Execute);
    }
    
    public void ExitState(ScreenState state) {}
}