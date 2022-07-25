using UnityEngine;

public class RemoveBalls : MonoBehaviour
{
    [SerializeField] private Transform _rack;
    private const float DELAY = .5f;
    private int _ballsCount;

    private void Start()
    {
        _ballsCount = _rack.GetComponentsInChildren<Ball>().Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            if (ball is CueBall)
                EventsManager.OnGameOver?.Invoke();

            SoundManager.PlaySound(SoundManager.Sound.Pocket);

            Destroy(collision.gameObject, DELAY);
            _ballsCount--;

            if (_ballsCount == 0)
                EventsManager.OnGameWin?.Invoke();
        }
    }
}
