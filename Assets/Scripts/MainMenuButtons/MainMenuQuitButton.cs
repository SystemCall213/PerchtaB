using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuQuitButton : MonoBehaviour
{
    public void Quit()
    {
        GetComponent<ButtonClick>().Play();
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false; // stop play mode in editor
        #else
                Application.Quit(); // quit standalone build
        #endif
    }
}
