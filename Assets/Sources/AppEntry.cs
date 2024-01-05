using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour {
    private IStateProvider _states;

    [Inject]
    private void Construct(IStateProvider states, CommandFactory commandFactory, PresenterFactory presenterFactory, StateChangeHandler handler) {
        _states = states;

        handler
            .AddMap<BootScreenState>(presenterFactory.AddPresenter<BootStatePresenter>)
            .AddMap<MenuScreenState>((map) => {
                commandFactory.AddRouteMap<PlayGameButton, LoadingScreenState>(map);
                commandFactory.AddRouteMap<ShowSettingsButton, SettingsScreenState>(map);
                
                presenterFactory.AddPresenter<ProfilePresenter>(map);
            })
            .AddMap<SettingsScreenState>(commandFactory.AddRouteBack<ReturnBackButton>)
            .AddMap<LoadingScreenState>(presenterFactory.AddPresenter<LoadingStatePresenter>)
            .AddMap<GameScreenState>((map) => {
                commandFactory.AddRouteMap<ShowPauseButton, PauseScreenState>(map);
                
                presenterFactory.AddPresenter<ProfilePresenter>(map);
            })
            .AddMap<UnLoadingScreenState>(presenterFactory.AddPresenter<UnLoadingStatePresenter>)
            .AddMap<PauseScreenState>((map) => {
                commandFactory.AddRouteMap<ReturnMenuButton, UnLoadingScreenState>(map);
                commandFactory.AddRouteBack<ReturnBackButton>(map);
            })
            .AddMap<CompleteScreenState>((map) => {
                commandFactory.AddRouteMap<ReturnMenuButton, UnLoadingScreenState>(map);
                commandFactory.AddRouteMap<NextGameButton, LoadingScreenState>(map);
            })
            .Complete();
    }
    
    private void Start() {
        _states.ChangeState<BootScreenState>();
    }
}
