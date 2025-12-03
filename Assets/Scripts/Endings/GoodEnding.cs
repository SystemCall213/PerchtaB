using System.Collections;
using UnityEngine;

public class GoodEnding : MonoBehaviour
{
    public string sceneToTransitionTo;

    void Start()
    {
        StartCoroutine(GoToTitles());
    }

    private IEnumerator GoToTitles()
    {
        yield return new WaitForSeconds(10f);
        SceneFader.Instance.FadeToScene(sceneToTransitionTo);
    }
}
