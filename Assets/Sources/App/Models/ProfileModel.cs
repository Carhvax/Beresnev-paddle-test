public class ProfileModel : IAppModel {
    
    public IObservableValue<int> BestScore { get; } = new ObservableValue<int>(0);
    public IObservableValue<int> Score { get; } = new ObservableValue<int>(0);
    public IObservableValue<int> Level { get; } = new ObservableValue<int>(1);
    public IObservableValue<float> Progress { get; } = new ObservableValue<float>(0);
    public IObservableValue<bool> Win { get; } = new ObservableValue<bool>(false);

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
        Win.Value = false;
    }
    
    public void AddScore() {
        Score.Value++;
    }

    public void AffectScore() {
        if (Score.Value > BestScore.Value)
            BestScore.Value = Score.Value;
    }
    
    public void WinGame() {
        Score.Value *= 2;
        Win.Value = true;
    }
}

public struct ProfileState {
    public int bestScore;
    public int score;
    public int level;
    public float progress;
}
