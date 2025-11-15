using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;
    private float fadeInDuration = 2f;
    private Image deathScreen;
    public GameObject restartButton;

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
        deathScreen = GetComponent<Image>();
    }

    public void Death()
    {
        deathScreen.enabled = true;
        restartButton.SetActive(true);

        StartCoroutine(FadeDeathScreen());
    }

    public IEnumerator FadeDeathScreen()
    {
        float startAlpha = deathScreen.color.a;
        float t = 0f;

        Image restartBtnImg = restartButton.GetComponent<Image>();

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            print(t);
            float a = Mathf.Lerp(startAlpha, 255, t / fadeInDuration);

            Color c = deathScreen.color;
            c.a = a / 255f;
            deathScreen.color = c;

            Color c2 = restartBtnImg.color;
            c2.a = a / 255f;
            restartBtnImg.color = c2;

            yield return null;
        }
    }
}
