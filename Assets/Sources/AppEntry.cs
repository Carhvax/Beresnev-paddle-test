using UnityEngine;
using Zenject;

public class AppEntry : MonoBehaviour {
    private IStateProvider _states;

    [Inject]
    private void Construct(IStateProvider states) {
        _states = states;
    }
    
    private void Start() {
        _states.ChangeState<BootScreenState>();
    }
}
