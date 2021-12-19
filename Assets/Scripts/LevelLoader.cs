using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public AttemptCounter attemptCounter;
    // public TextMeshProUGUI attemptCounterCanvas;
    
    private void Start()
    {
        //attemptCounterCanvas = GetComponentInChildren<TextMeshProUGUI>();
        if(!attemptCounter)
        {
            attemptCounter = FindObjectOfType<AttemptCounter>();
        }
    }
  
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void ReloadLevel()
    {
        StartCoroutine(WaitForReloadLevel(SceneManager.GetActiveScene().buildIndex));
        attemptCounter.AttemptsCountMinus();
        Debug.Log("Reload Level");
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
