using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // Called from a button, e.g., "Play" or "Back to Menu"
    public void LoadScene(string sceneName)
    {
        if (SceneFader.Instance != null)
        {
            SceneFader.Instance.FadeToScene(sceneName);
        }
        else
        {
            Debug.LogWarning("No SceneFader instance found in the scene!");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
