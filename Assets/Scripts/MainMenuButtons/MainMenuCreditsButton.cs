using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuCreditsButton : MonoBehaviour
{
    public GameObject titles;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = titles.GetComponent<Animator>();
        spriteRenderer = titles.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            animator.enabled = false;
            spriteRenderer.enabled = false;
        }
    }
    public void ButtonClick()
    {
        StopAllCoroutines();
        StartCoroutine(PlayStartAnimation());
    }

    private IEnumerator PlayStartAnimation()
    {
        animator.enabled = true;
        spriteRenderer.enabled = true;

        animator.Play("Titles", 0, 0f);
        animator.Update(0f);

        // wait until animation fully plays
        var state = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length - 0.08f);

        animator.enabled = false;
        spriteRenderer.enabled = false;
    }
}
