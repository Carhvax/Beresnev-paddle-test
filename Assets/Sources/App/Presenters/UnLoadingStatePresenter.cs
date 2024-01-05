public class UnLoadingStatePresenter : IStatePresenter {
    private readonly IMenuCommand _menuCommand;

    public UnLoadingStatePresenter(CommandFactory factory) {
        _menuCommand = factory.CreateRoute<MenuScreenState>();
    }
    
    public void EnterState(ScreenState state) {
        Delay.Execute(2f, _menuCommand.Execute);
    }
    
    public void ExitState(ScreenState state) {}
}
