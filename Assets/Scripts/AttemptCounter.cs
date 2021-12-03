using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AttemptCounter : MonoBehaviour
{
    public int numberOfAttempts = 5;
    public TextMeshProUGUI attemptText;
    public GameObject gameOverCanvas;
    public GameObject backCanvas;

    private void FixedUpdate()
    {
        attemptText.text = "ATTEMPT:" + numberOfAttempts.ToString();
    }

    public void AttemptsCountMinus()
    {
        numberOfAttempts -= 1;
        if (numberOfAttempts <= 0)
        {
            GameOver();
        }
    }
    // public void AttemptsCountsAdditional()
    // {
    //     numberOfAttempts += 1;
    // }
    public void GameOver()
    {
        if(numberOfAttempts == 0)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            gameOverCanvas.SetActive(true);
            backCanvas.SetActive(false);
            //PlayerPrefs.DeleteAll();
        }
    }
}
