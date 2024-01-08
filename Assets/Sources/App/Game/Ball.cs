using System;
using UnityEngine;

public interface IAimTarget {
    Vector3 Point { get; }
}

public class Ball : MonoBehaviour, IAimTarget {
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private BallModel _model;
    
    private Vector3 _velocity;
    private Vector3 _defaultPosition;
    public Vector3 Point => transform.position;
    

    public event Action<Ball> BallOut;
    public event Action<Ball, Paddle> BallReflected;

    private void Awake() => _defaultPosition = transform.position;

    public void Reset() {
        _velocity = Vector3.zero;
        transform.position = _defaultPosition;
    }
    
    public void AddForce(Vector3 direction) => _velocity = new Vector3(direction.x, 0, direction.z);

    public void InputTick() {
        
        if (GetObstacles(out var hit) && hit.collider.TryGetComponent<IReflective>(out var reflective)) {
            AddForce(reflective.Reflect(_velocity, hit.normal));
            
            if(reflective is Paddle paddle) {
                _model.SetColor(paddle.Color);
                BallReflected?.Invoke(this, paddle);
            }
        }
        
        MoveInstance();
    }
    private bool GetObstacles(out RaycastHit hit) => Physics.Raycast(transform.position, _velocity.normalized, out hit, .5f, _collisionMask);

    private void MoveInstance() {
        transform.Translate(_velocity * Time.deltaTime);
        
        var length = Mathf.Clamp(_velocity.magnitude - Time.deltaTime * 10f, 10, 20);
        _velocity = _velocity.normalized * length;

        if (Mathf.Abs(transform.position.z) >= 9) {
            BallOut?.Invoke(this);
        }
    }
}
