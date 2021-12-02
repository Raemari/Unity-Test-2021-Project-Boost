using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] LevelLoader levelLoader;

    public AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start()
    {
         audioSource = GetComponent<AudioSource>();
         levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();

    }

     void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
        levelLoader.LoadNextLevel();
        //LevelLoader.instance.LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        // to do add particile effect upon sucess
        GetComponent<Movement>().enabled = false;

        levelLoader.LoadNextLevel();
        //GetComponent<LevelLoader>().LoadNextLevel();
        //FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        // to do add particile effect upon crash
        GetComponent<Movement>().enabled = false;

        levelLoader.ReloadLevel();
        //GetComponent<LevelLoader>().ReloadLevel();
        //FindObjectOfType<LevelLoader>().ReloadLevel();

    }
}
