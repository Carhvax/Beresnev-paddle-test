using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BackgroundScroller : MonoBehaviour {
    private static readonly int _textureID = Shader.PropertyToID("_MainTex_ST");
    
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private float _speed = 1;
    
    private MaterialPropertyBlock _block;
    private Vector4 _offset = new Vector4(50, 50, 0, 0);
    
    private void Awake() => _block = new MaterialPropertyBlock();

    private void ChangeOffset(float amount) {
        _renderer.GetPropertyBlock(_block);
        _offset.w += amount;
        _block.SetVector(_textureID, _offset);
        _renderer.SetPropertyBlock(_block);
    } 
    
    private void OnEnable() {
        _offset = new Vector4(50, 50, 0, 0);
    }

    private void Update() {
        ChangeOffset(_speed * Time.deltaTime);
    }
    
    public void SetSpeed(float value) {
        _speed = value;
    }
}
