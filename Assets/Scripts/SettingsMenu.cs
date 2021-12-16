using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System;

public class SettingsMenu : MonoBehaviour
{
    private Transform menuPanel;
    private Event keyEvent;
    private TextMeshProUGUI buttonText;
    public TextMeshProUGUI thrustText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;
    private KeyCode newKey;
    private bool waitingForKey;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string  volumeParameter = "Volume";
    [SerializeField] Slider slider;
    [SerializeField] float multiplier = 30f;
    [SerializeField] Toggle toggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown qualityDropdown;
    private bool disableToggleEvent;
    private Resolution[] resolutions;


    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, slider.value);
    }
    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
        disableToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        disableToggleEvent = false;
    }
    private void HandleToggleValueChanged(bool enableSound)
    {
        if(disableToggleEvent)
            return;
        if(enableSound)
            slider.value = slider.maxValue;
        else
            slider.value = slider.minValue;
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter, slider.value);

        resolutions = Screen.resolutions;
        ResolutionDropDown();

        int Quality = PlayerPrefs.GetInt("qualityIndex", 0);
        qualityDropdown.value = Quality;

        menuPanel = transform.Find("Panel");
        waitingForKey = false;

        //iteration to find the child inside parent
        for(int i = 0; i < menuPanel.childCount; i++)
        {
             //Finding the child with that specific name
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
        //updates the current text assigned for that button
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
            //if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            //code above is wrong because the screen reso 
            //gets the desktop reso not the game's current reso
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
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

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
