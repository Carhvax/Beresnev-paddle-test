using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour {
    private IStateProvider _states;

    [Inject]
    private void Construct(IStateProvider states, CommandFactory commandFactory, PresenterFactory presenterFactory, StateChangeHandler handler) {
        _states = states;

        handler
            .AddMap<BootScreenState>((map) => {
                presenterFactory.AddPresenter<BootStatePresenter>(map);
            })
            .AddMap<MenuScreenState>((map) => {
                
            })
            .Complete();
    }
    
    private void Start() {
        _states.ChangeState<BootScreenState>();
    }
}
