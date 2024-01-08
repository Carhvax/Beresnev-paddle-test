using DG.Tweening;
using UnityEngine;

public enum PaddleOwnership { Player, Ai }

public interface IReflective {
    Vector3 Reflect(Vector3 affectPosition, Vector3 normal);
}

public class Paddle : MonoBehaviour, IReflective {
    [SerializeField] private Transform _model;
    [SerializeField] private PaddleOwnership _ownership;
    [SerializeField] private float _movementSpeed = 10f;
    
    public PaddleOwnership Ownership => _ownership;
    
    public void Move(float max, float point) {
        MoveInternal(max, point, _movementSpeed);
    }

    public void Idle(float max) {
        MoveInternal(max, 0, _movementSpeed * .3f);
    }
    
    private void MoveInternal(float max, float target, float speed) {
        var position = transform.position;
        var nextPosition = new Vector3(Mathf.Clamp(target, -max, max), position.y, position.z);
        transform.position = Vector3.MoveTowards(position, nextPosition, Time.deltaTime * speed);
    }

    public Vector3 Reflect(Vector3 affectPosition, Vector3 normal) {
        ThrowBack(affectPosition);

        return Vector3.Reflect(affectPosition.normalized + Vector3.right * .25f, normal) * affectPosition.magnitude;
    }
    
    private void ThrowBack(Vector3 affectPosition) {
        var back = _model.localPosition;
        var direction = (back - _model.TransformVector(affectPosition)).normalized * .5f;

        DOTween
            .Sequence()
            .Append(_model.DOLocalMove(back + direction, .1f))
            .Append(_model.DOLocalMove(back, .1f))
            .Play();
    }
}