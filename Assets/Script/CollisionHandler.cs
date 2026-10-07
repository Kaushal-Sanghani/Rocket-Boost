using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelReloadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollidable = true;   

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        RespondToDebugkeys();
    }

    void RespondToDebugkeys()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            Nextlevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    } 


    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Level Start");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Nextlevel", levelReloadDelay);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelReloadDelay);
    }

    void Nextlevel()
    {
        int currentScence = SceneManager.GetActiveScene().buildIndex;
        int NextScence = currentScence + 1;
        if (NextScence == SceneManager.sceneCountInBuildSettings)
        {
            NextScence = 0;
        }
        SceneManager.LoadScene(NextScence);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}


