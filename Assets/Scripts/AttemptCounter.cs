using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptCounter : MonoBehaviour
{
    public int numberOfAttempts = 10;
    public TextMeshProUGUI attemptText;
    //public GameObject attemptCanvas;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitForGameOverScreen());
        //attemptText = attemptCanvas.GetComponentInChildren<TextMeshProUGUI>();
        //attemptText = FindObjectOfType<TextMeshProUGUI>();
    }

    private void Update()
    {
        SetCurrentAttempts(numberOfAttempts);
        //attemptText.text = "ATTEMPT:" + numberOfAttempts.ToString();
    }
    private void SetCurrentAttempts(int numberofAttempts)
    {
        attemptText.SetText($"ATTEPT: {numberOfAttempts}");
    }
    public void SetOriginalAttemptNumber()
    {
        numberOfAttempts = 10;
    }

    public void AttemptsCountMinus()
    {
        numberOfAttempts -= 1;
        Debug.Log("Attempt counter minus");
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
            yield return null;
        }
        // yield return null;
    }
}
