using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    public void StartGame()
    {
        SceneFader.Instance.FadeToScene("FirstRoom");
    }
}
