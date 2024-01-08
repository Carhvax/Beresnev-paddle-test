using System.Linq;
using UnityEngine;

public interface IPaddleProvider {
    Vector3 TransformDirection(Vector3 direction);
}

public class ContentProvider : MonoBehaviour, IPaddleProvider {

    [SerializeField] private BackgroundScroller _scroller;
    [SerializeField] private CameraDirector _director;

    [Space]
    [SerializeField] private Wall[] _walls;
    [SerializeField] private Paddle[] _paddles;
    [SerializeField] private Ball _ball;
    
    private IInputController[] _controllers;
    private bool _isPlay;
    private IFieldEventListener _listener;

    private Paddle _nextPaddle;
    
    public void PrepareField(IFieldEventListener listener) {
        _listener = listener;
        _scroller.SetSpeed(2);
        _director.FitToScreen();
        _walls.Each(w => w.Init(this));
        _paddles.Each(p => p.Init(this));
    }

    public Vector3 TransformDirection(Vector3 direction) {
        return Vector3.Lerp(direction, _nextPaddle.transform.position, .25f);
    }
    
    public void HandleControllers(IInputController[] controllers) {
        _controllers = controllers;
        _controllers.Each(controller => _paddles.Each(controller.HandlePaddle));
    }

    private void Update() {
        if (!_isPlay) return;
        
        _controllers.Each(c => c.InputTick());
        _ball.InputTick();
    }

    public void Play() {
        _isPlay = true;

        var paddle = _paddles.GetRandom();

        _ball.BallOut += OnBallComeOut;
        _ball.BallReflected += OnBallReflected;
        
        _ball.AddForce(paddle.transform.position - _ball.transform.position);
        
        _nextPaddle = _paddles.FirstOrDefault(p => p != paddle);
    }
    
    private void OnBallReflected(Ball ball, Paddle paddle) {
        _listener.HandleBallReflection(ball, paddle);

        _nextPaddle = _paddles.FirstOrDefault(p => p != paddle);
    }

    private void OnBallComeOut(Ball ball) {
        _listener.HandleBallOut(ball);
        
        EndField();
    }
    
    public void EndField() {
        _isPlay = false;
        _scroller.SetSpeed(0);
        
        _ball.BallOut -= OnBallComeOut;
        _ball.BallReflected -= OnBallReflected;
        
        _paddles.Each(p => p.Reset());
        _ball.Reset();
    }

    public void Pause() {
        _isPlay = false;
        _scroller.SetSpeed(0);
    }
    
    public void Resume() {
        _isPlay = true;
        _scroller.SetSpeed(2);
    }
}
