using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AttemptCounter : MonoBehaviour
{
    public int numberOfAttempts = 10;
    public TextMeshProUGUI attemptText;
    public LevelLoader levelLoader;


    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        attemptText.text = "ATTEMPT:" + numberOfAttempts.ToString();
        StartCoroutine(WaitForGameOverScreen());
    }
    public void SetOriginalAttemptNumber()
    {
        numberOfAttempts = 3;
    }

    public void AttemptsCountMinus()
    {
        numberOfAttempts -= 1;
    }
    public void AddNumberOfAttempts()
    {
        numberOfAttempts += 5;
    }
    IEnumerator WaitForGameOverScreen()
    {
        while(numberOfAttempts <= 0)
        {
            GameManager.GM.ShowGameOverScreen();
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
            Debug.Log("COROUTINE");
        }
        yield return null;
    }
}
