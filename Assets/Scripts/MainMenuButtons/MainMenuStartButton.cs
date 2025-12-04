using System.Collections;
using UnityEngine;

public class MainMenuStart : MonoBehaviour
{
    public GameObject animator;
    public AudioClip startGameAnimMusicSource;

    public void StartGame()
    {
        GetComponent<ButtonClick>().Play();
        AudioManager.Instance.StopSoundEffectMusic();
        StartCoroutine(PlayStartAnimation()); 
    }

    private IEnumerator PlayStartAnimation()
    {
        AudioManager.Instance.PlayMusic(startGameAnimMusicSource, false);

        animator.GetComponent<SpriteRenderer>().enabled = true;
        Animator anim = animator.GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("StartGameAnimation");

        // wait until animation fully plays
        var state = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(state.length - 0.08f);

        anim.enabled = false;

        SceneFader.Instance.FadeToScene("FirstRoom");
    }
}
