using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CueBall : MonoBehaviour
{
    [SerializeField] private float _forceValue;
    [SerializeField] private Button _kickButton;

    private Vector3 _direction;
    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        EventsManager.OnDirectionChoosed += GetForce;
        _kickButton.onClick.AddListener(Kick);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Kick()
    {
        _rigidbody.AddForce((_direction - transform.position) * _forceValue, ForceMode.Impulse);
        EventsManager.OnBallCicked?.Invoke();
    }

    private void GetForce(Vector3 direction)
    {
        _direction = direction;
    }

    private void OnDisable()
    {
        EventsManager.OnDirectionChoosed -= GetForce;
        _kickButton.onClick.RemoveListener(Kick);
    }
}
