using UnityEngine;

public class Wall : MonoBehaviour, IReflective {

    public Vector3 Reflect(Vector3 affectPosition, Vector3 normal) {
        
        return Vector3.Reflect(affectPosition.normalized, normal) * affectPosition.magnitude;
    }
}
