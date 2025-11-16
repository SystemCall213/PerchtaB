using UnityEngine;

public class ButtonTransitionScene : MonoBehaviour
{
    public string sceneToTransitionTo;
    public static ButtonTransitionScene Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        gameObject.SetActive(false);
    }

    public void Transition()
    {
        SceneFader.Instance.FadeToScene(sceneToTransitionTo);
    }
}
