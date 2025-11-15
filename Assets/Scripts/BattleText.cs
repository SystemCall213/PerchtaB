using System.Collections;
using TMPro;
using UnityEngine;

public class BattleText : MonoBehaviour
{
    public static BattleText Instance;
    private TextMeshProUGUI textHolder;
    public float typeSpeed = 0.03f;
    private Coroutine typingRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        if (typingRoutine != null)
            StopCoroutine(typingRoutine);

        typingRoutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string text)
    {
        textHolder.text = "";

        foreach (char c in text)
        {
            textHolder.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void Disable()
    {
        textHolder.enabled = false;
    }

    public void Enable()
    {
        textHolder.enabled = true;
    }

}
