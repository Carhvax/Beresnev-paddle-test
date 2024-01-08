public class ProfileModel : IAppModel {
    
    public IObservableValue<int> BestScore { get; } = new ObservableValue<int>(0);
    public IObservableValue<int> Score { get; } = new ObservableValue<int>(0);
    public IObservableValue<int> Level { get; } = new ObservableValue<int>(1);
    public IObservableValue<float> Progress { get; } = new ObservableValue<float>(0);

    public ProfileState State {
        
        get => new ProfileState() {
            level = Level.Value,
            bestScore = BestScore.Value,
            score = Score.Value,
            progress = Progress.Value,
        };
        set {
            Level.Value = value.level;
            BestScore.Value = value.bestScore;
            Score.Value = value.score;
            Progress.Value = value.progress;
        }
    }

    public void ResetScore() {
        Score.Value = 0;
    }
    
    public void AddScore() {
        Score.Value++;
    }

    public void AffectScore() {
        if (Score.Value > BestScore.Value)
            BestScore.Value = Score.Value;
    }
}

public struct ProfileState {
    public int bestScore;
    public int score;
    public int level;
    public float progress;
}
