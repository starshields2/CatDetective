using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public bool muted;
   private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    public TMP_Dropdown resolutionDropdown;
    public int currentResolutionIndex;

    void Start()
    {
        //Screen.SetResolution(1920, 1080, true);
        resolutions = Screen.resolutions;
        currentRefreshRate = Screen.currentResolution.refreshRate;
        resolutionDropdown.ClearOptions();
        filteredResolutions = new List<Resolution>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }



        List<string> options = new List<string>();
        for(int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);

            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetDropdownResolution(int selectedIndex)
    {
        selectedIndex = resolutionDropdown.value;
        Resolution resolution = filteredResolutions[selectedIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        Screen.fullScreen = false;
    }

   public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

public void SetMasterVolume(float volume)
    {
        volume = Mathf.Log10(volume) * 20f;
        Debug.Log(volume);
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Sound", volume);
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Music", volume);
    }

    public void MuteAll()
    {
        if (muted)
        {
            SetMasterVolume(1f);
            Debug.Log("Volume set to: something");
            muted = false;
        }
        else
        {
            SetMasterVolume(0f);
            Debug.Log("Volume set to: something else.");
            muted = true;
        }
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
