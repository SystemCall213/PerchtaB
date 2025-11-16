using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private TextMeshProUGUI speakerComponent;
    [SerializeField] private float textSpeed;
    [SerializeField] private ChoiceContainer choiceContainer;
    [SerializeField] private TextAsset jsonFile;

    private List<DialogueLine> lines;
    private Dictionary<string, int> lineMap;
    private int index;
    private bool waitingForChoice = false;

    private void Awake()
    {
        textComponent.text = string.Empty;
        speakerComponent.text = string.Empty;

        LoadDialogueFromFile();
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
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
