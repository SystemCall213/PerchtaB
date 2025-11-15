using UnityEngine;

public class OptionsBackMenuButton : MonoBehaviour
{
    public Canvas optionsCanvas;

    public void BackToMenu()
    {
        optionsCanvas.enabled = false;
    }
}
