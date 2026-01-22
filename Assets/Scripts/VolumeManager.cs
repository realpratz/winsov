using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private Slider volumeSlider;
    private const string VolumePrefKey = "GlobalVolume";

    public void click()
    {
        if(volumePanel.activeSelf) volumePanel.SetActive(false);
        else volumePanel.SetActive(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);

        AudioListener.volume = savedVolume;

        volumeSlider.value = savedVolume;
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value; 

        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }
}
