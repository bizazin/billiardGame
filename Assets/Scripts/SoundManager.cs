using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        BallCollision = 0,
        CueCollision = 1,
        CushionCollision = 2,
        Lose = 3,
        Pocket = 4,
        Rack = 5,
        Win = 6
    }

    public static void PlaySound(Sound sound)
    {
        var soundObject = new GameObject("Sound");
        var audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (var soundAudioClip in GameAssets.Instance.SoundAudioClips)
            if (soundAudioClip.Sound == sound)
                return soundAudioClip.AudioClip;
        Debug.Log("Sound " + sound + " not found!");
        return null;
    }
}
