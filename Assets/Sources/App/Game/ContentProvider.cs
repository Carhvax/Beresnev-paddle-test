using System;
using UnityEngine;

public class ContentProvider : MonoBehaviour {

    [SerializeField] private BackgroundScroller _scroller;
    [SerializeField] private CameraDirector _director;

    [Space]
    [SerializeField] private Paddle[] _paddles;
    private IInputController[] _controllers;
    private bool _isPlay;

    public void PrepareField() {
        _scroller.SetSpeed(2);
        _director.FitToScreen();
    }
    
    public void HandleControllers(IInputController[] controllers) {
        _controllers = controllers;
        _controllers.Each(controller => _paddles.Each(controller.HandlePaddle));
    }

    private void Update() {
        if (!_isPlay) return;
        
        _controllers.Each(c => c.InputTick());
    }

    public void Play() {
        _isPlay = true;
    }
    
    public void Pause() {
        _isPlay = false;
        _scroller.SetSpeed(0);
    }
    
    public void Resume() {
        _isPlay = true;
        _scroller.SetSpeed(2);
    }
    
    public void EndField() {
        _isPlay = false;
        _scroller.SetSpeed(0);
    }

    
}
