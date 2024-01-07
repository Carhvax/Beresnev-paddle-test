public interface IInputController {
    void HandlePaddle(Paddle paddle);

    void InputTick();
}

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

public class AiInput : IInputController {
    private readonly Ball _ball;

    private Paddle _paddle;
    private float _velocity;

    public AiInput(Ball ball) {
        _ball = ball;
    }
    
    public void HandlePaddle(Paddle paddle) {
        if (paddle.Ownership == PaddleOwnership.Ai)
            _paddle = paddle;
    }
    
    public void InputTick() {
        _paddle.Move(4, _ball.Point.x);
    }
}