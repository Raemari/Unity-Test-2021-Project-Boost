using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AttemptCounter : MonoBehaviour
{
    public int numberOfAttempts = 5;
    public TextMeshProUGUI attemptText;
    public LevelLoader levelLoader;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        attemptText.text = "ATTEMPT:" + numberOfAttempts.ToString();
    }

    public void AttemptsCountMinus()
    {
        numberOfAttempts -= 1;
        if (numberOfAttempts <= 0)
        {
            Debug.Log("GAME OVER RUN");
            levelLoader.GetComponent<LevelLoader>().ShowGameOverScreen();
        }
    }
}
