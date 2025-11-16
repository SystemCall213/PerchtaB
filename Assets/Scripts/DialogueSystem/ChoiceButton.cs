using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceText;
    private Choice myChoice;
    private Dialogue dialogue;

    public void Setup(Choice choice, Dialogue dialogueRef)
    {
        myChoice = choice;
        dialogue = dialogueRef;
        choiceText.text = choice.text;

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void DisableButtonInteraction()
    {
        GetComponent<Button>().interactable = false;
    }

    private void OnClick()
    {
        dialogue.OnChoiceSelected(myChoice.nextLineId);
        if (myChoice.setFlag != null) 
        {
            PlayerFlags.Instance.SetFlag(myChoice.setFlag);
        }
        if (myChoice.requiredFlag != null) 
        {
            PlayerFlags.Instance.RemoveFlag(myChoice.requiredFlag);
        }
    }
}
