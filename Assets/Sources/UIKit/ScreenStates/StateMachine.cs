using System.Collections.Generic;
using System.Linq;

public interface IScreenState {
    void Show();
    void Hide();
}

public interface IStateProvider {
    bool HasHistory { get; }
    void ChangeState<T>() where T : class, IScreenState;
    void Back();
}

namespace ScreenStates {
    
    public class StateMachine<TState> : IStateProvider where TState : class, IScreenState {

        private readonly HashSet<TState> _states = new();
        private readonly Stack<TState> _history = new();

        private TState _current;
    
        public bool HasHistory => _history.Count > 0;
        public bool CurrentStateIs<T>() where T : class, TState => _current is T;

        protected void AddState(TState state) {
            _states.Add(state);
        }

        public void ChangeState<T>() where T : class, IScreenState {
            var state = _states.OfType<T>().FirstOrDefault();
        
            if(_current != null) _history.Push(_current);

            ChangeState(state as TState);
        }

        public void Back() {
            if (!HasHistory) return;
        
            ChangeState(_history.Pop());
        }
    
        private void ChangeState(TState state) {
            _current?.Hide();
            _current = state;
            _current?.Show();
        }
    }

    internal static class ScreenStatesExtensions {
        
    }
}


