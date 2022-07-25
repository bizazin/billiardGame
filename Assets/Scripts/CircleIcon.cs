using UnityEngine;

public class CircleIcon : MonoBehaviour
{
    [SerializeField] private Sprite _circleSprite;
    public float SpriteRadius => _circleSprite.bounds.size.x / 2;

    public void SetIconPosition(Vector3 direction)
    {
        transform.position = direction;
    }
}
