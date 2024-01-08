using UnityEngine;

public class Wall : MonoBehaviour, IReflective {
    private IPaddleProvider _paddleSource;

    public Vector3 Reflect(Vector3 affectPosition, Vector3 normal) {
        var reflection = Vector3.Reflect(affectPosition.normalized, normal) * affectPosition.magnitude;
        return _paddleSource.TransformDirection(reflection);
    }
    
    public void Init(IPaddleProvider paddleSource) {
        _paddleSource = paddleSource;
    }
}
