public class UnLoadingStatePresenter : IStatePresenter {
    
    private readonly GameController _controller;
    private readonly IMenuCommand _menuCommand;

    public UnLoadingStatePresenter(CommandFactory factory, GameController controller) {
        _controller = controller;
        _menuCommand = factory.CreateRoute<MenuScreenState>();
    }
    
    public void EnterState(ScreenState state) {
        
        Delay.Execute(2f, _menuCommand.Execute);
    }
    
    public void ExitState(ScreenState state) {}
}

public class CompleteStatePresenter : IStatePresenter {

    private readonly ProfileModel _model;
    private readonly GameController _controller;

    public CompleteStatePresenter(ProfileModel model, GameController controller) {
        _model = model;
        _controller = controller;
    }
    
    public void EnterState(ScreenState state) {
        _controller.EndGame();

        if (state.OnResolvePresenterView<CompletePresenterView>(out var view)) {
            view.Win(_model.Win.Value);
        }
    }

    public void ExitState(ScreenState state) {}
}