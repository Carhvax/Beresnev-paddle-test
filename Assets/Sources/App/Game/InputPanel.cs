using UnityEngine;
using UnityEngine.EventSystems;

public class InputPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    
    private Plane _plane;
    private Camera _camera;
    private bool _hold;

    public Vector3 Point { get; private set; }

    private void Awake() {
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update() {
        if(!_hold) return;
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_plane.Raycast(ray, out var distance))
            Point = ray.GetPoint(distance);
    }
    
    public void OnPointerDown(PointerEventData eventData) => _hold = true;
    public void OnPointerUp(PointerEventData eventData) => _hold = false;
}
