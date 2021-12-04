using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //public static LevelLoader instance;
    public Animator transition;
    public float transitionTime = 4f;
    public AttemptCounter attemptCounter;
  
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadLevel()
    {
        StartCoroutine(WaitForReloadLevel(SceneManager.GetActiveScene().buildIndex));
        attemptCounter.GetComponent<AttemptCounter>().AttemptsCountMinus();
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator WaitForReloadLevel(float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
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
    }
}
