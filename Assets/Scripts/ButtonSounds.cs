using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioSource sfxPlayer;

    public void playSFX(int idx)
    {
        stopAll();
        sfxPlayer.PlayOneShot(sfx[idx]);
    }

    public void stopAll()
    {
        sfxPlayer.Stop();
    }
}
