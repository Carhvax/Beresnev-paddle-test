public class GameController {
    private readonly ContentProvider _content;
    private readonly ProfileModel _model;

    public GameController(ContentProvider content, ProfileModel model) {
        _content = content;
        _model = model;
    }
    
    public void PrepareGame() {
        _model.ResetScore();
        _content.PrepareField();
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
