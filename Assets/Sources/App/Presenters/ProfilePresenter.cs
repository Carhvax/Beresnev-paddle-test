public class ProfilePresenter :  IStatePresenter {
    private readonly ProfileModel _model;
    private ProfilePresenterView _presenter;

    public ProfilePresenter(ProfileModel model) {
        _model = model;
    }
    
    public void EnterState(ScreenState state) {
        if (state.OnResolvePresenterView<ProfilePresenterView>(out var presenter)) {
            _presenter = presenter;
            
            _model.Level.Changed += OnLevelChanged;
            _model.BestScore.Changed += OnBestScoreChanged;
            _model.Score.Changed += OnScoreChanged;
            _model.Progress.Changed += OnProgressChanged;
        }
    }
    private void OnProgressChanged(float value) => _presenter.UpdateProgress(value);

    private void OnScoreChanged(int value) => _presenter.UpdateCurrentScore(value);

    private void OnBestScoreChanged(int value) => _presenter.UpdateBestScore(value);

    private void OnLevelChanged(int value) => _presenter.UpdateLevel(value);

    public void ExitState(ScreenState state) {
        _model.Level.Changed -= OnLevelChanged;
        _model.BestScore.Changed -= OnBestScoreChanged;
        _model.Score.Changed -= OnScoreChanged;
        _model.Progress.Changed -= OnProgressChanged;
    }
}
