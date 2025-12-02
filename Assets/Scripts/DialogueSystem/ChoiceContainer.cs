using System.Collections.Generic;
using UnityEngine;

public class ChoiceContainer : MonoBehaviour
{
    [SerializeField] private ChoiceButton choicePrefab;

    public void ShowChoices(List<Choice> choices, Dialogue dialogue)
    {
        // Remove old buttons
        KillChildren();

        foreach (var choice in choices)
        {
            bool allowed = string.IsNullOrEmpty(choice.requiredFlag)
                            || PlayerFlags.Instance.HasFlag(choice.requiredFlag);

            ChoiceButton button = Instantiate(choicePrefab, transform);
            button.Setup(choice, dialogue);

            if (!allowed)
            {
                button.DisableButtonInteraction();
            }
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void KillChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
