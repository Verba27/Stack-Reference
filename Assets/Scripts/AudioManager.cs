using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] private AudioClip scoredSound;
    [SerializeField] private AudioClip endGameSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlaySound(int i)
    {
        switch (i)
        {
            case 0:
                audio.clip = scoredSound;
                break;
            case 1:
                audio.clip = endGameSound;
                break;
        }
        audio.Play();
    }
}
