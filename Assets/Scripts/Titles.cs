using System.Collections;
using UnityEngine;

public class Titles : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneFader.Instance.FadeToScene("MainMenu");
        }
    }
    private void Start()
    {
        StartCoroutine(PlayStartAnimation());
    }

    private IEnumerator PlayStartAnimation()
    {
        Animator anim = animator.GetComponent<Animator>();

        // wait until animation fully plays
        var state = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length - 0.08f);

        anim.enabled = false;

        SceneFader.Instance.FadeToScene("MainMenu");
    }
}
