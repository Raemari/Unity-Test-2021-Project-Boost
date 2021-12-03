using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int levelIsUnlocked;
    public Button[] levelButtons;

    private void Start()
    {
        //1 is for the default unlocked level
        levelIsUnlocked = PlayerPrefs.GetInt("levelIsUnlocked", 1);
        for ( int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        for ( int i = 0; i < levelIsUnlocked; i++)
        {
            levelButtons[i].interactable = true;
        }
    }
    public void OpenLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
