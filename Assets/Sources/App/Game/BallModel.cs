using UnityEngine;

public class BallModel : ColoredModel {
    public void SetColor(Color color) {
        color *= .5f;
        AddColor(color);
    }
}
