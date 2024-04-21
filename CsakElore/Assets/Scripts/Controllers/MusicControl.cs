using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicControl : MonoBehaviour
{
    //Zene
    private static MusicControl instance;

    private AudioSource audioSource;
    public AudioClip[] Songs;
    public float Volume;
    private float trackTimer;
    private float songPlayed;
    private bool[] beenPlayed;



    void Awake()
    {

        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        beenPlayed = new bool[Songs.Length];
        if (!audioSource.isPlaying)
        {
            ChangeSong(Random.Range(0, Songs.Length));
        }



    }

    void Update()
    {
        audioSource.volume = Volume;
        if (audioSource.isPlaying)
        {
            trackTimer += 1 * Time.deltaTime;
        }

        if (!audioSource.isPlaying || trackTimer >= audioSource.clip.length)
        {
            ChangeSong(Random.Range(0, Songs.Length));
        }
        if (songPlayed == Songs.Length)
        {
            songPlayed = 0;
            for (int i = 0; i < Songs.Length; i++)
            {
                if (i == Songs.Length)
                {
                    break;

                }
                else
                {
                    beenPlayed[i] = false;
                }
            }
        }
    }

    public void ChangeSong(int songPicked)
    {
        if (!beenPlayed[songPicked])
        {
            trackTimer = 0;
            songPlayed++;
            beenPlayed[songPicked] = true;
            audioSource.clip = Songs[songPicked];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }



}