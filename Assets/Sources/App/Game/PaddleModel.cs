using UnityEngine;

public class PaddleModel : ColoredModel {
    
    [SerializeField] private Color _color = Color.white;
    public Color Color => _color;

    private void Start() {
        AddColor(_color);
    }
}
