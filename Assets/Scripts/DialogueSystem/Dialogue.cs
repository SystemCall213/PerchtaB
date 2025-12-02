using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI speakerComponent;
    [SerializeField] private float textSpeed;
    [SerializeField] private ChoiceContainer choiceContainer;
    [SerializeField] private TextAsset jsonFile;
    [SerializeField] private Image girl;
    [SerializeField] private Image perchta;

    private List<DialogueLine> lines;
    private Dictionary<string, int> lineMap;
    private int index;
    private bool waitingForChoice = false;

    public void StartDialogue()
    {
        textComponent.text = string.Empty;
        speakerComponent.text = string.Empty;
        StartCoroutine(FadeInGraphic(perchta, 1f));
        StartCoroutine(FadeInGraphic(girl, 1f));
        girl.enabled = true;
        perchta.enabled = true;

        LoadDialogueFromFile();
        index = 0;
        StartCoroutine(TypeLine());
    }

    private IEnumerator FadeInGraphic(Image graphic, float duration)
    {
        Color c = graphic.color;
        c.a = 0f;
        graphic.color = c;

        graphic.enabled = true;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            c.a = Mathf.Lerp(0f, 1f, t);
            graphic.color = c;

            yield return null;
        }

        c.a = 1f;
        graphic.color = c;
    }


    IEnumerator TypeLine()
    {
        DialogueLine currentLine = lines[index];

        textComponent.text = string.Empty;
        if (currentLine.speaker != null)
        {
            speakerComponent.text = currentLine.speaker;
        }
        else
        {
            speakerComponent.text = string.Empty;
        }

        if (currentLine.text != null)
        {
            foreach (char c in currentLine.text.ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        // Check for choices after typing finishes
        if (currentLine.choices != null && currentLine.choices.Count > 0)
        {
            waitingForChoice = true;
            choiceContainer.ShowChoices(currentLine.choices, this);
        }
    }

    void Update()
    {
        if (waitingForChoice) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index].text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].text;
                // Check for choices after typing finishes
                if (lines[index].choices != null && lines[index].choices.Count > 0)
                {
                    waitingForChoice = true;
                    choiceContainer.ShowChoices(lines[index].choices, this);
                }
            }
        }
    }

    void NextLine()
    {
        DialogueLine currentLine = lines[index];

        // Check if this line explicitly points to a next one
        if (!string.IsNullOrEmpty(currentLine.nextLineId))
        {
            if (lineMap.TryGetValue(currentLine.nextLineId, out int next))
            {
                index = next;
                while (index < lines.Count)
                {
                    index++;
                }
            }
            else
            {
                Debug.LogWarning($"Next line ID {currentLine.nextLineId} not found.");
                gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            index++;

            if (index >= lines.Count)
            {
                gameObject.SetActive(false);
                girl.enabled = false;
                perchta.enabled = false;
                if (StickerShow.Instance != null) StickerShow.Instance.SetTransitionActive(true);
                return;
            }
        }

        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void OnChoiceSelected(string nextLineId)
    {
        choiceContainer.KillChildren();

        if (lineMap.TryGetValue(nextLineId, out int nextIndex))
        {
            index = nextIndex;
            waitingForChoice = false;
            StartCoroutine(TypeLine());
        }
        else
        {
            Debug.LogWarning("Next line ID not found: " + nextLineId);
        }
    }

    void LoadDialogueFromFile()
    {
        DialogueData data = new DialogueData();
        if (jsonFile != null)
        {
            string jsonText = jsonFile.text;
            data = JsonUtility.FromJson<DialogueData>(jsonText);
        }
        lines = data.lines;

        // Optional: build ID lookup map
        lineMap = new Dictionary<string, int>();
        for (int i = 0; i < lines.Count; i++)
        {
            if (!string.IsNullOrEmpty(lines[i].id))
            {
                lineMap[lines[i].id] = i;
            }
        }
    }
}
