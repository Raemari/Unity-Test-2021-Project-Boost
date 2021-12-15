using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private Transform menuPanel;
    private Event keyEvent;
    private TextMeshProUGUI buttonText;
    public TextMeshProUGUI thrustText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    private KeyCode newKey;
    private bool waitingForKey;


    void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDropDown();
        menuPanel = transform.Find("Panel");
        waitingForKey = false;

        for(int i = 0; i < menuPanel.childCount; i++) // iteration para mahanap yung children sa loob
        {
             //hinahanap yung child na May ganitong pangalan
            if(menuPanel.GetChild(i).name == "ThrustKey")
                menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.thrust.ToString(); // setting the text kung ano i-seset mo
            else if(menuPanel.GetChild(i).name == "LeftKey")
                menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.left.ToString();
            else if(menuPanel.GetChild(i).name == "RightKey")
                menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.right.ToString();
        }
    }
    private void Update()
    {
        if(PlayerPrefs.HasKey("thrustKey"))
        {
            thrustText.text = PlayerPrefs.GetString("thrustKey");
        }
        if(PlayerPrefs.HasKey("leftKey"))
        {
            leftText.text = PlayerPrefs.GetString("leftKey");
        }
        if(PlayerPrefs.HasKey("rightKey"))
        {
            rightText.text = PlayerPrefs.GetString("rightKey");
        }
    }
    private void OnGUI()
    {
        //assigning the keys if we're only waitingforkey
        keyEvent = Event.current;
        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
                        Debug.Log("ONGUI");
        }
    }
    public void StartAssignment(string keyName)
    {
        if(!waitingForKey)
        {
            StartCoroutine(AssignKey(keyName));
            Debug.Log("Start Asssignment");
        }
    }
    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text; //updates the text on the button that was clicked
    }
    IEnumerator WaitForKey()
    {
        while(!keyEvent.isKey)
        yield return null;
        Debug.Log("Wait for key");
    }
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        yield return WaitForKey();
                    Debug.Log("Assign key");

        switch(keyName)
        {
            case "thrust":
                GameManager.GM.thrust = newKey;
                buttonText.text = GameManager.GM.thrust.ToString();
                PlayerPrefs.SetString("thrustKey", GameManager.GM.thrust.ToString());
                            Debug.Log("switch case thurst");
                break;
            case "left":
                GameManager.GM.left = newKey;
                buttonText.text = GameManager.GM.left.ToString();
                PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString());
                break;
            case "right":
                GameManager.GM.right = newKey;
                buttonText.text = GameManager.GM.right.ToString();
                PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString());
                break;
        }
        yield return null;
    }

    public void ResolutionDropDown()
    {
        int currentResolutionIndex = 0;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for(int i= 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetVolume(float volume)
    {   //the string volume corresponds to the naming of exposed parameter for Audio Mixer
        audioMixer.SetFloat("Volume", volume);
        Debug.Log("SOUND ADJUST");
    }
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
