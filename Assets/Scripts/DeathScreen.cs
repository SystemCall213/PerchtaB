using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;
    public GameObject killingAnimationObject;
    private float fadeInDuration = 2f;
    private Image deathScreen;
    public GameObject restartButton;
    public GameObject gotoMainMenuButton;

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
        StartCoroutine(DieWithAnimation());
    }

    public IEnumerator DieWithAnimation()
    {
        if (PlayerPrefs.GetInt("DisableViolence") == 0)
        {
            killingAnimationObject.GetComponent<SpriteRenderer>().enabled = true;
            Animator anim = killingAnimationObject.GetComponent<Animator>();
            anim.enabled = true;
            anim.Play("KillingAnimation");

            // wait until animation fully plays
            var state = anim.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(state.length - 0.04f);

            anim.enabled = false;
        }

        deathScreen.enabled = true;
        restartButton.SetActive(true);
        gotoMainMenuButton.SetActive(true);

        StartCoroutine(FadeDeathScreen());
    }

    public IEnumerator FadeDeathScreen()
    {
        float startAlpha = deathScreen.color.a;
        float t = 0f;

        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            print(t);
            float a = Mathf.Lerp(startAlpha, 255, t / fadeInDuration);

            Color c = deathScreen.color;
            c.a = a / 255f;
            deathScreen.color = c;

            yield return null;
        }
    }
}
