using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // need to double check for shorter version
    public void PlayLevel1()
    {
    SceneManager.LoadScene(1);
    }
        public void PlayLevel2()
    {
    SceneManager.LoadScene(2);
    }
        public void PlayLevel3()
    {
    SceneManager.LoadScene(3);
    }
    public void PlayLevel4()
    {
        SceneManager.LoadScene(4);
    }
    public void PlayLevel5()
    {
        SceneManager.LoadScene(5);
    }
    
    public void ExitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }
    public void AdjustSettings()
    {

    }
    public void Help()
    {

    }
    public void showCredits()
    {

    }
}
