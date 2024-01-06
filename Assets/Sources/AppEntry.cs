using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour {
    
    private IStateProvider _states;
    private IIOService _ioService;
    private IMenuCommand _pauseCommand;
    private ProfileModel _model;

    [Inject]
    private void Construct(IIOService ioService, ProfileModel model, IStateProvider states, CommandFactory commandFactory, PresenterFactory presenterFactory, StateChangeHandler handler) {
        _model = model;
        _ioService = ioService;
        _states = states;
        
        _ioService.Load();
        _pauseCommand = commandFactory.CreateRoute<ShowPauseButton, PauseScreenState>();
        
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
                map.AddObserver(_pauseCommand as IStateObserver);
                
                presenterFactory.AddPresenter<ProfilePresenter>(map);
            })
            .AddMap<UnLoadingScreenState>(presenterFactory.AddPresenter<UnLoadingStatePresenter>)
            .AddMap<PauseScreenState>((map) => {
                commandFactory.AddRouteMap<ReturnMenuButton, UnLoadingScreenState>(map);
                commandFactory.AddRouteMap<RestartMenuButton, LoadingScreenState>(map);
                commandFactory.AddRouteBack<ReturnBackButton>(map);
                
                presenterFactory.AddPresenter<ProfilePresenter>(map);
                presenterFactory.AddPresenter<PauseStatePresenter>(map);
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

    private void OnApplicationPause(bool state) {
        if (state && _states.CurrentStateIs<GameScreenState>()) {
            _pauseCommand.Execute();
        }
        
        _ioService.Save();
    }
    
    private void OnApplicationFocus(bool state) {
        if (!state && _states.CurrentStateIs<GameScreenState>()) {
            _pauseCommand.Execute();
        }
        
        _ioService.Save();
    }

    [Button]
    private void TestScore() {
        _model.Score.Value += 10;
        _ioService.Save();
    }
}
