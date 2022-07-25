using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball _) && collision.relativeVelocity.magnitude > 1)
            SoundManager.PlaySound(SoundManager.Sound.BallCollision);
        if (collision.gameObject.TryGetComponent(out Cushions _) && collision.relativeVelocity.magnitude > 1)
            SoundManager.PlaySound(SoundManager.Sound.CushionCollision);
    }
}
