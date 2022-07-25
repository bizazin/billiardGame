using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Trajectory))]
public class CueBall : Ball
{
    [SerializeField] private float _forceValue;
    [SerializeField] private Button _kickButton;

    private const float TIME_TO_STOP = 9;

    private Rigidbody _rigidbody;
    private Trajectory _ray;

    private void OnEnable()
    {
        _kickButton.onClick.AddListener(Kick);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _ray = GetComponent<Trajectory>();
    }

    private void Kick()
    {
        SoundManager.PlaySound(SoundManager.Sound.CueCollision);

        var pos = _ray.Direction - transform.position;
        var relativeDir = pos / pos.magnitude;
        _rigidbody.AddForce(relativeDir * _forceValue, ForceMode.Impulse);

        EventsManager.OnBallKicked?.Invoke();

        StartCoroutine(WaitUntilStop());
    }


    private IEnumerator WaitUntilStop()
    {
        yield return new WaitForSeconds(TIME_TO_STOP);

        ResetRotation();
        EventsManager.OnBallStopped?.Invoke();
    }

    private void ResetRotation()
    {
        transform.eulerAngles = Vector3.zero;
    }

    private void OnDisable()
    {
        _kickButton.onClick.RemoveListener(Kick);
    }
}

