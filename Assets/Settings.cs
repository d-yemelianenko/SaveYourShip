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

    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        res = resolutions.Distinct().ToArray();
        string[] strRes = new string[res.Length];

        for(int i = 0; i < res.Length; i++)
        {
            strRes[i] = res[i].width.ToString() + "x" + resolutions[i].height.ToString();
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(strRes.ToList());
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionPreference", res.Length - 1);
        resolutionDropdown.value = savedResolutionIndex;
        Screen.SetResolution(res[savedResolutionIndex].width, res[savedResolutionIndex].height, true);
        LoadSettings(savedResolutionIndex);
        Debug.Log("Heja");
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

    }
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(res[resolutionDropdown.value].width, res[resolutionDropdown.value].height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
    }
    public void SetQulity(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityPreference", qualityIndex);
    }
    public void SetSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", System.Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.Save();
    }
    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
    }
}
