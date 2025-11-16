using UnityEngine;

public class MainMenuOprionsButton : MonoBehaviour
{
    public Canvas optionsCanvas;

    public void ViewOptions()
    {
        GetComponent<ButtonClick>().Play();
        optionsCanvas.enabled = true;
    }
}
