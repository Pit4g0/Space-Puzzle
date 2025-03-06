using UnityEngine;



public class SoundsManager : MonoBehaviour
{
    [SerializeField] AudioClip OpenDoor;
    [SerializeField] AudioClip TimerStop;
    [SerializeField] AudioClip TakePuzzle;
    [SerializeField] AudioClip Error;
    [SerializeField] AudioClip PutPuzzle;
    [SerializeField] AudioClip Finish;
    [SerializeField] AudioClip BackgroundMusic;

    private AudioSource audioSource;

    public enum Sounds
    {
        OpenDoor,
        TimerStop,
        TakePuzzle,
        Error,
        PutPuzzle,
        Finish,
        BackgroundMusic
    }

    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("SoundsManager").Length >1)
        {
            Destroy(this.gameObject); 
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);

        audioSource.Play();
    }

    public void PlaySound(Sounds sounds)
    {
        switch (sounds)
        {
            case Sounds.OpenDoor:
                audioSource.PlayOneShot(OpenDoor);
                break;
            case Sounds.TimerStop:
                audioSource.PlayOneShot(TimerStop);
                break;
            case Sounds.TakePuzzle:
                audioSource.PlayOneShot(TakePuzzle);
                break;
            case Sounds.PutPuzzle:
                audioSource.PlayOneShot(PutPuzzle);
                break;
            case Sounds.Error:
                audioSource.PlayOneShot(Error);
                break;
            case Sounds.Finish:
                audioSource.PlayOneShot(Finish);
                break;
            case Sounds.BackgroundMusic:
                audioSource.PlayOneShot(BackgroundMusic);
                break;
            default:
                break;
        }
    }
}
