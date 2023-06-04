using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    Resolution[] res;

    //Resolution[] resolutions;
    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        res = resolutions.Distinct().ToArray();
        string[] strRes = new string[res.Length];
        //resolutionDropdown.ClearOptions();
        //List<string> options = new List<string>();
        //resolutions = Screen.resolutions;
        //int currentResolutionIndex = 0;

        for(int i = 0; i < res.Length; i++)
        {
            // strRes[i] = res[i].ToString();
            strRes[i] = res[i].width.ToString() + "x" + resolutions[i].height.ToString();
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(strRes.ToList());
        resolutionDropdown.value = res.Length - 1;
        Screen.SetResolution(res[res.Length - 1].width, res[res.Length - 1].height, true);
        //{
        //    string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
        //    options.Add(option);
        //    if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = 1;
        //    }

        //    resolutionDropdown.AddOptions(options);
        //    resolutionDropdown.RefreshShownValue();
        //    LoadSettings(currentResolutionIndex);

    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(res[resolutionDropdown.value].width, res[resolutionDropdown.value].height, true);
        //Resolution resolution = resolutions[resolutionIndex];
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
    public void SetQulity(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        //PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;

        //if (PlayerPrefs.HasKey("ResolutionPreference"))
        //    resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
        //else
        //    resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
    }
}
