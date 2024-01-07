using System.Collections.Generic;
using System.Linq;

public class GameController {
    
    private readonly ContentProvider _content;
    private readonly ProfileModel _model;
    private readonly IInputController[] _controllers;

    public GameController(ContentProvider content, ProfileModel model, IEnumerable<IInputController> controllers) {
        _content = content;
        _model = model;
        _controllers = controllers.ToArray();
    }
    
    public void PrepareGame() {
        _model.ResetScore();
        _content.PrepareField();
        _content.HandleControllers(_controllers);
    }
    
    public void PlayGame() {
        _content.Play();
    }

    public void PauseGame() {
        _content.Pause();
    }

    public void ResumeGame() {
        _content.Resume();
    }

    public void EndGame() {
        _content.EndField();
    }
}
