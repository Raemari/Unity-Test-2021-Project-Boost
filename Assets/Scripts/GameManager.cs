using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    public KeyCode thrust {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}

    //private bool areAllLevelsDone;
    public GameObject attemptCounterCanvas;
    //private int firstTimeTofinish;
    //private int levelsFinished;
    //private int levelChecker;
    

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

    void Start()
    {
        //CheckIfFirstTimeToFinish();
        //FinishedAllLevels();
    }
    void Update()
    {
        //FinishedAllLevels();
        ClearPlayerPrefs();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CheckIfFirstTimeToFinish();
        }
    }

    public void CheckIfFirstTimeToFinish()
    {
        if (PlayerPrefs.GetInt("firstTimeToFinish", 1) == 1)
        {
            Debug.Log("FIRST TIME TO FINISH");
            PlayerPrefs.SetInt("firstTimeToFinish", 0);
            ShowGameFinish();
            DisableAttemptCounter();
        }
        else
        {
            Debug.Log("Already finished");
        }
    }

    public void DisableAttemptCounter()
    {
        if(attemptCounterCanvas.activeInHierarchy)
        {
            attemptCounterCanvas.SetActive(false);
            Debug.Log("DISABLE THE ATTEMPT COUNTER");
        }
    }

    // private void FinishedAllLevels() // parang nonsense ahhaa
    // {
    //     if(levelChecker == 5)
    //     {
    //         levelChecker = levelsFinished;
    //         areAllLevelsDone = true;
    //         Debug.Log("FINISHED THE GAMe");
    //     }
    // }
    private void ShowGameFinish()
    {
        SceneManager.LoadScene(7); // finish game scene
    }
    public void ClearPlayerPrefs()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("DELETE ALL PLAYERPREFS");
        }
    }
}
