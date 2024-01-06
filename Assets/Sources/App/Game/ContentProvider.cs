using UnityEngine;

public class ContentProvider : MonoBehaviour {

    [SerializeField] private BackgroundScroller _scroller;
    [SerializeField] private CameraDirector _director;
    
    public void PrepareField() {
        _scroller.SetSpeed(2);
        _director.FitToScreen();
    }
    
    public void Play() {
        
    }
    
    public void Pause() {
        _scroller.SetSpeed(0);
    }
    
    public void Resume() {
        _scroller.SetSpeed(2);
    }
    
    public void EndField() {
        _scroller.SetSpeed(0);
    }
    
}
