using UnityEngine;

public enum PaddleOwnership { Player, Ai }

public class Paddle : MonoBehaviour {
    [SerializeField] private PaddleOwnership _ownership;
    [SerializeField] private float _movementSpeed = 5f;
    
    public PaddleOwnership Ownership => _ownership;
    
    public void Move(float max, float point) {
        var position = transform.position;
        var nextPosition = new Vector3(Mathf.Clamp(point, -max, max), position.y, position.z);
        transform.position = Vector3.MoveTowards(position, nextPosition, Time.deltaTime * _movementSpeed);
    }
}
