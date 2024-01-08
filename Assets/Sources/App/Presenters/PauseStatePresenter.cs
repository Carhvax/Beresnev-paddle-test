public class PauseStatePresenter : IStatePresenter {
    private readonly GameController _controller;

    public PauseStatePresenter(GameController controller) => _controller = controller;

    public void EnterState(ScreenState state) => _controller.PauseGame();
    
    public void ExitState(ScreenState state) => _controller.ResumeGame();
}
