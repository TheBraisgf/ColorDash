using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public AudioSource menuMusic;

    void Start()
    {
        if (menuMusic != null && !menuMusic.isPlaying)
        {
            menuMusic.Play(); // 🔊 Reproduce la música
        }
    }
}
