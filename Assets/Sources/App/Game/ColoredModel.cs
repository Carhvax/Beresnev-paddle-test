using UnityEngine;

public abstract class ColoredModel : MonoBehaviour {
    private static readonly int ColorField = Shader.PropertyToID("_Color");
    
    [SerializeField] private MeshRenderer _renderer;
    
    private MaterialPropertyBlock _block;
    
    private void OnValidate() {
        _renderer = GetComponent<MeshRenderer>();
    }
    
    private void Awake() {
        _block = new MaterialPropertyBlock();
    }
    
    protected void AddColor(Color color) {
        _block.SetColor(ColorField, color);
        _renderer.SetPropertyBlock(_block);
    }
}
