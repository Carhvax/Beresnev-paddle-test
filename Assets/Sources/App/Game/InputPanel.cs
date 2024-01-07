using UnityEngine;

public class InputPanel : MonoBehaviour {
    
    private Vector3 _holdStart;
    private Plane _plane;
    private Camera _camera;

    public Vector3 Point { get; private set; }

    private void Awake() {
        _camera = Camera.main;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void Update() {
        if(!Input.GetMouseButton(0)) return;
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_plane.Raycast(ray, out var distance))
            Point = ray.GetPoint(distance);
    }
}
