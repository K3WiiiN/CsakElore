using UnityEngine;

public class SFXControl : MonoBehaviour
{
    // Hangeffektek
    public AudioSource audioSource;
    public AudioClip Jump;
    public AudioClip Click;
    public AudioClip Start;
    public AudioClip Finish;
    public AudioClip Gameover;
    public AudioClip Boost;
    public float Volume;

 
    void Awake()
    { 
        audioSource.volume = Volume;
    }
   

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
