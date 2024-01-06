public class UnLoadingStatePresenter : IStatePresenter {
    private readonly GameController _controller;
    private readonly IMenuCommand _menuCommand;

    public UnLoadingStatePresenter(CommandFactory factory, GameController controller) {
        _controller = controller;
        _menuCommand = factory.CreateRoute<MenuScreenState>();
    }
    
    public void EnterState(ScreenState state) {
        _controller.EndGame();
        Delay.Execute(2f, _menuCommand.Execute);
    }
    
    public void ExitState(ScreenState state) {}
}
