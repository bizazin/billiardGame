using UnityEngine;
using UnityEngine.UI;

public class NotInterectableMode : MonoBehaviour
{
    [SerializeField] private CircleIcon _circleIcon;
    [SerializeField] private LineRenderer _trajectory;
    [SerializeField] private LineRenderer _hittedTrajecotory;
    [SerializeField] private Button _kickButton;

    private void OnEnable()
    {
        EventsManager.OnBallStopped += EnableInteraction;
        EventsManager.OnBallKicked += DisableInteraction;
    }

    public void EnableInteraction()
    {
        _circleIcon.gameObject.SetActive(true);
        _trajectory.gameObject.SetActive(true);
        _hittedTrajecotory.gameObject.SetActive(true);
        _kickButton.gameObject.SetActive(true);
    }

    public void DisableInteraction()
    {
        _circleIcon.gameObject.SetActive(false);
        _trajectory.gameObject.SetActive(false);
        _hittedTrajecotory.gameObject.SetActive(false);
        _kickButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventsManager.OnBallStopped -= EnableInteraction;
        EventsManager.OnBallKicked -= DisableInteraction;
    }
}
