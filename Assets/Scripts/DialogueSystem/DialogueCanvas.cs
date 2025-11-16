using System.Collections;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public Dialogue dialogue;
    public float startDialogueDelay = 3f;

    void Start()
    {
        dialogue.gameObject.SetActive(false);

        StartDialogue();
    }

    public void StartDialogue()
    {
        StartCoroutine(StartWithDelay());
    }

    private IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(startDialogueDelay);

        dialogue.gameObject.SetActive(true);
        dialogue.StartDialogue();
    }
}
