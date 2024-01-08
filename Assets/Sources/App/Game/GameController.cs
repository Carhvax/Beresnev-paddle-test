using System;
using System.Collections.Generic;
using System.Linq;

public interface IFieldEventListener {
    void HandleBallOut(Ball ball);
    void HandleBallReflection(Ball ball, Paddle paddle);
}

public class GameController : IFieldEventListener {
    
    private readonly ContentProvider _content;
    private readonly ProfileModel _model;
    private readonly IInputController[] _controllers;

    public event Action GameEnd;
    
    public GameController(ContentProvider content, ProfileModel model, IEnumerable<IInputController> controllers) {
        _content = content;
        _model = model;
        _controllers = controllers.ToArray();
    }
    
    public void PrepareGame() {
        _model.ResetScore();
        _content.PrepareField(this);
        _content.HandleControllers(_controllers);
    }

    public void HandleBallOut(Ball ball) => GameEnd?.Invoke();

    public void HandleBallReflection(Ball ball, Paddle paddle) {
        if (paddle.Ownership == PaddleOwnership.Player)
            _model.AddScore();
    }
    
    public void PlayGame() => _content.Play();

    public void PauseGame() => _content.Pause();

    public void ResumeGame() => _content.Resume();

    public void EndGame() {
        _content.EndField();
        _model.AffectScore();
    }
}
