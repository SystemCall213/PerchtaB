using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        print(value);
        AudioManager.Instance.masterVolumeMultiplier = value;
        AudioManager.Instance.UpdateVolume();
    }
}
