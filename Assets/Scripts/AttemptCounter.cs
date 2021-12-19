using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttemptCounter : MonoBehaviour
{
    [SerializeField] int originalNumberOfAttempts = 10;
    [SerializeField] int attemptsLeft;

    [SerializeField] TextMeshProUGUI attemptText;


    private void Awake()
    {
        attemptsLeft = PlayerPrefs.GetInt("AttemptsLeft",originalNumberOfAttempts);
        originalNumberOfAttempts = attemptsLeft;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(WaitForGameOverScreen());
        UpdateUI();
    }

    private void Update()
    {
        // UpdateUI();
    }

    private void UpdateUI()
    {
        attemptText.text = "ATTEMPT:" + attemptsLeft.ToString();
    }

    public void SetOriginalAttemptNumber()
    {
        originalNumberOfAttempts = 10;
        UpdateUI();
    }

    public void AttemptsCountMinus()
    {
        PlayerPrefs.SetInt("AttemptsLeft",originalNumberOfAttempts--);
        Debug.Log("Attempt counter minus");
        UpdateUI();
    }
    public void AddNumberOfAttempts()
    {
        //not sure if this is possible
        //PlayerPrefs.SetInt("AttemptsLeft",originalNumberOfAttempts+5);
        originalNumberOfAttempts += 5;
        UpdateUI();
    }
    IEnumerator WaitForGameOverScreen()
    {
        while(originalNumberOfAttempts == 0)
        {
            GameManager.GM.ShowGameOverScreen();
            yield return new WaitForSeconds(2f);
            Time.timeScale = 0;
            Debug.Log("COROUTINE");
            yield return null;
            PlayerPrefs.DeleteAll();
        }
        // yield return null;
    }
}
