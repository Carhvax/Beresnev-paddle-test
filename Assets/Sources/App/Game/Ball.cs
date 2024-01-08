using UnityEngine;

public interface IAimTarget {
    Vector3 Point { get; }
}

public class Ball : MonoBehaviour, IAimTarget {
    [SerializeField] private LayerMask _collisionMask;
    
    public Vector3 Point => transform.position;
    private Vector3 _velocity;
    
    public void Reset() {
        _velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
    
    public void AddForce(Vector3 direction) {
        _velocity = direction;
    }

    public void InputTick() {
        
        if (GetObstacles(out var hit) && hit.collider.TryGetComponent<IReflective>(out var reflective)) {
            AddForce(reflective.Reflect(_velocity, hit.normal));
        }
        
        MoveInstance();
    }
    private bool GetObstacles(out RaycastHit hit) {
        return Physics.Raycast(transform.position, _velocity.normalized, out hit, .5f, _collisionMask);
    }

    private void MoveInstance() {
        transform.Translate(_velocity * Time.deltaTime);
        
        var length = Mathf.Clamp(_velocity.magnitude - Time.deltaTime * 5f, 10, 20);
        _velocity = _velocity.normalized * length;

        if (Mathf.Abs(transform.position.z) > 8) {
            Debug.Log("Out!");
        }
    }
}
