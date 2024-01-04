using ScreenStates;

public class AppStates : StateMachine<ScreenState> {

    public AppStates(ScreenState[] states) {
        states.Each(AddState);
    }

}
