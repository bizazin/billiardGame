using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform _restartPanel;
    [SerializeField] private RectTransform _tutorialPanel;

    private Button _restartButton;
    private Button _tutorialCloseButton;

    private void OnEnable()
    {
        _restartButton = _restartPanel.GetComponentInChildren<Button>();
        _restartButton.onClick.AddListener(RestartGame);

        _tutorialCloseButton = _tutorialPanel.GetComponentInChildren<Button>();
        _tutorialCloseButton.onClick.AddListener(StartGame);

        EventsManager.OnGameOver += GameOver;
        EventsManager.OnGameWin += GameWin;
    }

    private void StartGame()
    {
        _tutorialPanel.gameObject.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.Rack);
    }

    private void GameOver()
    {
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        Restart();
    }

    private void GameWin()
    {
        SoundManager.PlaySound(SoundManager.Sound.Win);
        Restart();
    }

    private void Restart()
    {
        _restartPanel.gameObject.SetActive(true);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _tutorialCloseButton.onClick.RemoveListener(StartGame);

        EventsManager.OnGameOver -= GameOver;
        EventsManager.OnGameWin -= GameWin;
    }
}
