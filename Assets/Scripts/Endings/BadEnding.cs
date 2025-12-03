using System.Collections;
using UnityEngine;

public class BadEnding : MonoBehaviour
{
    public string sceneToTransitionTo;

    void Start()
    {
        StartCoroutine(BackToMainMenu());
    }

    private IEnumerator BackToMainMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneFader.Instance.FadeToScene(sceneToTransitionTo);
    }
}
