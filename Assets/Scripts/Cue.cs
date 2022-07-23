using DG.Tweening;
using UnityEngine;

public class Cue : MonoBehaviour
{
    [SerializeField] private float _endValue;
    [SerializeField] private float _duration;
    [SerializeField] private float _forceValue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            transform.DOMoveX(_endValue, _duration).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CueBall cueBall))
            cueBall.GetComponent<Rigidbody>().AddForceAtPosition(-transform.right * _forceValue, collision.contacts[0].point, ForceMode.VelocityChange);
    }
}
