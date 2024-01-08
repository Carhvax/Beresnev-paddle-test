using DG.Tweening;
using UnityEngine;

public class Paddle : MonoBehaviour, IReflective {
    
    [SerializeField] private PaddleModel _model;
    [SerializeField] private PaddleOwnership _ownership;
    [SerializeField] private float _movementSpeed = 10f;
    
    private Vector3 _defaultPosition;
    private IPaddleProvider _paddleSource;

    public PaddleOwnership Ownership => _ownership;
    public Color Color => _model.Color;
    
    private void Awake() => _defaultPosition = transform.position;

    public void Init(IPaddleProvider paddleSource) => _paddleSource = paddleSource;

    public void Move(float max, float point) => MoveInternal(max, point, _movementSpeed);

    public void Idle(float max) => MoveInternal(max, 0, _movementSpeed * .3f);

    private void MoveInternal(float max, float target, float speed) {
        var position = transform.position;
        var nextPosition = new Vector3(Mathf.Clamp(target, -max, max), position.y, position.z);
        transform.position = Vector3.MoveTowards(position, nextPosition, Time.deltaTime * speed);
    }

    public Vector3 Reflect(Vector3 affectPosition, Vector3 normal) {
        ThrowBack(affectPosition);
        
        var boost = affectPosition.magnitude * 1.5f;
        var reflection = Vector3.Reflect(affectPosition.normalized, normal) * boost;
        
        return _paddleSource.TransformDirection(reflection);
    }
    
    private void ThrowBack(Vector3 affectPosition) {
        var back = _model.transform.localPosition;
        var direction = (back - _model.transform.TransformVector(affectPosition)).normalized * .25f;

        DOTween
            .Sequence()
            .Append(_model.transform.DOLocalMove(back + direction, .1f))
            .Append(_model.transform.DOLocalMove(back, .1f))
            .Play();
    }
    
    public void Reset() => transform.position = _defaultPosition;
}