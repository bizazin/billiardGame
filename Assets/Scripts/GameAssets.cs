using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton

    public static GameAssets Instance;

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }
    #endregion

    public SoundAudioClip[] SoundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound Sound;
        public AudioClip AudioClip;
    }
}
