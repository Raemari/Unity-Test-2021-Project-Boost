using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip crashSound;
    private AudioSource audioSource;

    public KeyCode thrust {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}
    public AttemptCounter attemptCounter;
    public GameObject attemptCounterCanvas;
    

    private void Awake()
    {
        if(GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }
        thrust = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("thrustKey", "Space"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }
    private void Start()
    {
        attemptCounter = attemptCounterCanvas.GetComponent<AttemptCounter>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySuccessSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
    }
    public void PlayCrashSound()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
    }

    public void CheckIfFirstTimeToFinish()
    {
        // if (PlayerPrefs.GetInt("firstTimeToFinish", 1) == 1)
        // {
        //     Debug.Log("FIRST TIME TO FINISH");
        //     PlayerPrefs.SetInt("firstTimeToFinish", 0);
        //     ShowGameFinish();
        //     AdditionalAttempts();
        // }
        if(!PlayerPrefs.HasKey("firstTimeToFinish"))
        {
            Debug.Log("FIRST TIME TO FINISH");
            PlayerPrefs.SetInt("firstTimeToFinish", 0);
            ShowGameFinish();
            AdditionalAttempts();
        }
        else
        {
            ShowPlayAgain();
            Debug.Log("Already finished");
        }
    }

    public void AdditionalAttempts()
    {
        attemptCounter.AddNumberOfAttempts();
        Debug.Log("ALl levels done, add attempt number");
    }

    private void ShowGameFinish()
    {
        SceneManager.LoadScene(7); // finish game scene
        PlaySuccessSound();
    }
    private void ShowPlayAgain()
    {
        SceneManager.LoadScene(8); //Play again scene
        PlaySuccessSound();
    }
    public void ShowGameOverScreen()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Reset();
    }
    public void Reset()
    {
        //If back to main menu is clicked
        Time.timeScale = 1;
        PlayerPrefs.DeleteAll();
        Debug.Log("RESET ACTIVATED");
        attemptCounter.SetOriginalAttemptNumber();
    }
}
