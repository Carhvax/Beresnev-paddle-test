public class PlayerInput : IInputController {
    private readonly InputPanel _panel;

    private Paddle _paddle;
    private float _velocity;

    public PlayerInput(InputPanel panel) {
        _panel = panel;
    }
    
    public void HandlePaddle(Paddle paddle) {
        if (paddle.Ownership == PaddleOwnership.Player)
            _paddle = paddle;
    }
    
    public void InputTick() {
        _paddle.Move(4, _panel.Point.x);
    }
}
