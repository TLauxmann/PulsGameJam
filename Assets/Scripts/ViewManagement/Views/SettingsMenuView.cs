using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenuView : View
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Button _backButton;

    private Resolution[] _resolutions;

    private void Start()
    {
        AddScreenResolutionToDropdown();

        SetUpMusicSliders();

    }

    public override void Initialize()
    {
        _backButton.onClick.AddListener(() =>
        {
            PlayerPrefs.Save();
            ViewManager.ShowLast();
        });
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    private void AddScreenResolutionToDropdown()
    {
        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio.value + " Hz";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height &&
                _resolutions[i].refreshRateRatio.value == Screen.currentResolution.refreshRateRatio.value)
            {
                currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }
    private void SetUpMusicSliders()
    {

        //Set Sliders to audioMixer value
        _audioMixer.GetFloat(AudioManager.instance.masterVolExposed, out float masterVol);
        _audioMixer.GetFloat(AudioManager.instance.musicVolExposed, out float musicVol);
        _audioMixer.GetFloat(AudioManager.instance.sfxVolExposed, out float sfxVol);
        _masterSlider.value = Mathf.Pow(10, (masterVol / 20));
        _musicSlider.value = Mathf.Pow(10, (musicVol / 20));
        _sfxSlider.value = Mathf.Pow(10, (sfxVol / 20));

        _masterSlider.onValueChanged.AddListener((sliderValue) =>
        {
            float volume = Mathf.Log10(sliderValue) * 20f;
            _audioMixer.SetFloat(AudioManager.instance.masterVolExposed, volume);
            PlayerPrefs.SetFloat(AudioManager.instance.masterVolExposed, volume);
        });
        _musicSlider.onValueChanged.AddListener((sliderValue) =>
        {
            float volume = Mathf.Log10(sliderValue) * 20;
            _audioMixer.SetFloat(AudioManager.instance.musicVolExposed, volume);
            PlayerPrefs.SetFloat(AudioManager.instance.musicVolExposed, volume);
        });
        _sfxSlider.onValueChanged.AddListener((sliderValue) =>
        {
            float volume = Mathf.Log10(sliderValue) * 20;
            _audioMixer.SetFloat(AudioManager.instance.sfxVolExposed, volume);
            PlayerPrefs.SetFloat(AudioManager.instance.sfxVolExposed, volume);
        });
    }
}
