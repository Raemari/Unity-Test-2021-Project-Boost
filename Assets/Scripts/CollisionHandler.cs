using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    //This class is almost like my Game Manager
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] LevelLoader levelLoader;

    private AudioSource audioSource;
    public GameObject rocket;
    private float delayRocket = 1f;
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
        LevelUnlocked();
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
            case "End":
                GameManager.GM.CheckIfFirstTimeToFinish();
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
        LevelUnlocked();
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
        Invoke("DisableRocket", delayRocket);
        //StartCoroutine(WaitForRocketToDisable());

        //GetComponent<LevelLoader>().ReloadLevel();
        //FindObjectOfType<LevelLoader>().ReloadLevel();
    }
    private void LevelUnlocked()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if(currentLevel >= PlayerPrefs.GetInt("levelIsUnlocked"))
        {
            //unlocks the current level
            PlayerPrefs.SetInt("levelIsUnlocked", currentLevel + 1);
        }
        Debug.Log("LEVEL UNLOCKED");
    }
    private void DisableRocket()
    {
        if (rocket.activeInHierarchy)
        {
            rocket.SetActive(false);
        }
        else
        {
            rocket.SetActive(false);
        }
    }
}
