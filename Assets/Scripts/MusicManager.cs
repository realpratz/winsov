using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] private AudioSource musicPlayer;

    public void playMusic(int idx)
    {
        if(musicPlayer.isPlaying && musicPlayer.clip == music[idx])
        {
            return;
        }

        stopAll();
        musicPlayer.clip=music[idx];
        musicPlayer.Play();
    }

    public void stopAll()
    {
        musicPlayer.Stop();
    }
}
