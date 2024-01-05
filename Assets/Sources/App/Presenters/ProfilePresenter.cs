using UnityEngine;

public class ProfilePresenter :  IStatePresenter {

    public ProfilePresenter() {
        
    }
    
    public void EnterState(ScreenState state) {
        if (state.OnResolvePresenterView<ProfilePresenterView>(out var presenter)) {
            presenter.UpdateBestScore(99);
            presenter.UpdateCurrentScore(33);
            presenter.UpdateProgress(.5f, 15);
        }
    }
    
    public void ExitState(ScreenState state) {
        
    }
}
