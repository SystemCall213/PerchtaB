using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    public void StartGame()
    {
        print("efsee");
        SceneFader.Instance.FadeToScene("SampleScene");
    }
}
