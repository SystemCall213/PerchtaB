using System.Collections;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public Dialogue dialogue;
    public float startDialogueDelay = 3f;
    public float fadeDuration = 1f;

    private CanvasGroup dialogueGroup;

    void Start()
    {
        // Get the CanvasGroup on the dialogue object
        dialogueGroup = dialogue.GetComponent<CanvasGroup>();

        // Make sure it's invisible at the start
        dialogueGroup.alpha = 0f;
        dialogue.gameObject.SetActive(true); // Must be active for alpha to change

        StartDialogue();
    }

    public void StartDialogue()
    {
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        // Wait before starting the fade
        yield return new WaitForSeconds(startDialogueDelay);

        // Fade the dialogue group in
        yield return StartCoroutine(FadeIn(dialogueGroup));

        // After fade completes, start the dialogue logic
        dialogue.StartDialogue();
    }

    private IEnumerator FadeIn(CanvasGroup group)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }

        group.alpha = 1f;
    }

}
