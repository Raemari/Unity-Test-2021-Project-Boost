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
        //attemptCounter.GetComponent<AttemptCounter>().AttemptsCountsAdditional();
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
}
