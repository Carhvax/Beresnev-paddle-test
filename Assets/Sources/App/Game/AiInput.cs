public class AiInput : IInputController {
    
    private readonly IAimTarget _ball;

    private Paddle _paddle;
    private float _velocity;

    public AiInput(IAimTarget ball) {
        _ball = ball;
    }
    
    public void HandlePaddle(Paddle paddle) {
        if (paddle.Ownership == PaddleOwnership.Ai)
            _paddle = paddle;
    }
    
    public void InputTick() {
        var distance = (_paddle.transform.position - _ball.Point).magnitude;

        var inSeek = distance <= 6;
        
        if(inSeek) {
            _paddle.Move(4, _ball.Point.x);
            return;
        }
        
        _paddle.Idle(4);
    }
}
