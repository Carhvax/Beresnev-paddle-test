using UnityEngine;

public class CameraDirector : MonoBehaviour {
    private const float BASE_RATIO = 828f / 1792f;
    private const float BASE_HEIGHT = 20f;
    
    [SerializeField] private Transform _camera;
    
    public void FitToScreen() {
        var current = Screen.width / (float)Screen.height;
        var currentRatio =  BASE_RATIO / current;

        _camera.transform.position = new Vector3(0, BASE_HEIGHT * currentRatio, 0);
    }
}
