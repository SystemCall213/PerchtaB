using UnityEngine;
using UnityEngine.UI;

public class GoreOption : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        PlayerPrefs.SetInt("DisableViolence", 0);
    }

    private void OnToggleValueChanged(bool value)
    {
        PlayerPrefs.SetInt("DisableViolence", value ? 1 : 0);
    }
}
