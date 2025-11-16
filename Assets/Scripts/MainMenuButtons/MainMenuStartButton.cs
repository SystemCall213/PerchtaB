using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    public void StartGame()
    {
        GetComponent<ButtonClick>().Play();
        SceneFader.Instance.FadeToScene("FirstRoom");
    }
}
