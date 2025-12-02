using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public PanelExpander_Anchors battleField;
    public Enemy enemy;
    public RectTransform buttons;
    public RectTransform girlHp;
    public RectTransform garbageHp;
    public RectTransform textActions;
    public Image battlefield;
    public float animationDuration = 2f;
    private bool isExpanded = false;

    public static RoundManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ToggleButtons()
    {
        Button[] btns = buttons.GetComponentsInChildren<Button>();

        foreach (Button button in btns)
        {
            bool isInteractable = button.interactable;
            button.interactable = !isInteractable;
        }
    }

    public void Toggle()
    {
        if (Enemy.Instance.hPBar.CurrentHp() != 0)
        {
            if (isExpanded)
            {
                SpriteRenderer playerSprite = Player.Instance.GetComponent<SpriteRenderer>();
                StartCoroutine(hidePlayerSprite(playerSprite));
                BattleText.Instance.SetText("");
                ToggleButtons();
            }
            else
            {
                Transform playerTransform = Player.Instance.GetTransform();
                playerTransform.localPosition = new Vector3(0, 0, 0);
                SpriteRenderer playerSprite = Player.Instance.GetComponent<SpriteRenderer>();
                StartCoroutine(showPlayerSprite(playerSprite));
            }
            BattleText.Instance.SetText("");
            //battleField.TogglePanel();
            StartCoroutine(moveEnemy());
            StartCoroutine(moveButtons());
            StartCoroutine(moveText());
            //StartCoroutine(moveHpBars());
            if (!isExpanded) 
            {
                enemy.Execute();
                StartCoroutine(showBattlefield());
            }
            else
            {
                StartCoroutine(hideBattlefield());
            }
            isExpanded = !isExpanded;   
        }
    }

    private IEnumerator showPlayerSprite(SpriteRenderer playerSprite)
    {
        Color c = playerSprite.color;
        float elapsed = 0f;
        float duration = 2f; 

        c.a = 0f;
        playerSprite.color = c;
        playerSprite.enabled = true;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            c.a = Mathf.Lerp(0f, 1f, t);
            playerSprite.color = c;

            yield return null;
        }

        c.a = 1f;
        playerSprite.color = c;
    }

    private IEnumerator hidePlayerSprite(SpriteRenderer playerSprite)
    {
        Color c = playerSprite.color;
        float elapsed = 0f;
        float duration = 2f;

        c.a = 1f;
        playerSprite.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            c.a = Mathf.Lerp(1f, 0f, t);
            playerSprite.color = c;

            yield return null;
        }

        c.a = 0f;
        playerSprite.color = c;
    }

    private IEnumerator showBattlefield()
    {
        Color c = battlefield.color;
        float elapsed = 0f;
        float duration = 2f; 

        c.a = 0f;
        battlefield.color = c;
        battlefield.enabled = true;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            c.a = Mathf.Lerp(0f, 1f, t);
            battlefield.color = c;

            yield return null;
        }

        c.a = 1f;
        battlefield.color = c;
    }

    private IEnumerator hideBattlefield()
    {
        Color c = battlefield.color;
        float elapsed = 0f;
        float duration = 2f;

        c.a = 1f;
        battlefield.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            c.a = Mathf.Lerp(1f, 0f, t);
            battlefield.color = c;

            yield return null;
        }

        c.a = 0f;
        battlefield.color = c;
    }

    private IEnumerator moveEnemy()
    {
        RectTransform enemyTransform = enemy.GetRectTransform();

        Vector4 start = new Vector4(
            enemyTransform.offsetMin.x,  // left
            enemyTransform.offsetMin.y,  // bottom
            enemyTransform.offsetMax.x,  // right
            enemyTransform.offsetMax.y   // top
        );

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            float left;
            float bottom;
            float right;
            float top;

            if (!isExpanded)
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y - 600f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w - 600f, t);
            }
            else
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y + 600f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w + 600f, t);
            }

            enemyTransform.offsetMin = new Vector2(left, bottom);
            enemyTransform.offsetMax = new Vector2(right, top);

            yield return null;
        }

        if (!isExpanded)
        {
            enemyTransform.offsetMin = new Vector2(start.x, start.y - 600f);
            enemyTransform.offsetMax = new Vector2(start.z, start.w - 600f);
        }
        else
        {
            enemyTransform.offsetMin = new Vector2(start.x, start.y + 600f);
            enemyTransform.offsetMax = new Vector2(start.z, start.w + 600f);
        }
    }

    private IEnumerator moveButtons()
    {
        Vector4 start = new Vector4(
            buttons.offsetMin.x,  // left
            buttons.offsetMin.y,  // bottom
            buttons.offsetMax.x,  // right
            buttons.offsetMax.y   // top
        );

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            float left;
            float bottom;
            float right;
            float top;

            if (isExpanded)
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y - 700f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w - 700f, t);
            }
            else
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y + 700f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w + 700f, t);
            }

            buttons.offsetMin = new Vector2(left, bottom);
            buttons.offsetMax = new Vector2(right, top);

            yield return null;
        }

        if (isExpanded)
        {
            buttons.offsetMin = new Vector2(start.x, start.y - 700f);
            buttons.offsetMax = new Vector2(start.z, start.w - 700f);
        }
        else
        {
            buttons.offsetMin = new Vector2(start.x, start.y + 700f);
            buttons.offsetMax = new Vector2(start.z, start.w + 700f);
        }
    }

    private IEnumerator moveText()
    {
        Vector4 start = new Vector4(
            textActions.offsetMin.x,  // left
            textActions.offsetMin.y,  // bottom
            textActions.offsetMax.x,  // right
            textActions.offsetMax.y   // top
        );

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            float left;
            float bottom;
            float right;
            float top;

            if (!isExpanded)
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y + 500f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w + 500f, t);
            }
            else
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y - 500f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w - 500f, t);
            }

            textActions.offsetMin = new Vector2(left, bottom);
            textActions.offsetMax = new Vector2(right, top);

            yield return null;
        }

        if (!isExpanded)
        {
            textActions.offsetMin = new Vector2(start.x, start.y + 500f);
            textActions.offsetMax = new Vector2(start.z, start.w + 500f);
        }
        else
        {
            textActions.offsetMin = new Vector2(start.x, start.y - 500f);
            textActions.offsetMax = new Vector2(start.z, start.w - 500f);
        }
    }

    // deprecated
    private IEnumerator moveHpBars()
    {
        Vector4 girlStart = new Vector4(
            girlHp.offsetMin.x,  // left
            girlHp.offsetMin.y,  // bottom
            girlHp.offsetMax.x,  // right
            girlHp.offsetMax.y   // top
        );

        Vector4 garbageStart = new Vector4(
            garbageHp.offsetMin.x,  // left
            garbageHp.offsetMin.y,  // bottom
            garbageHp.offsetMax.x,  // right
            garbageHp.offsetMax.y   // top
        );

        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);

            float left;
            float bottom;
            float right;
            float top;

            if (!isExpanded)
            {
                left = Mathf.Lerp(girlStart.x, girlStart.x - 250f, t);
                bottom = girlStart.y;
                right = Mathf.Lerp(girlStart.z, girlStart.z - 250f, t);
                top = girlStart.w;
            }
            else
            {
                left = Mathf.Lerp(girlStart.x, girlStart.x + 250f, t);
                bottom = girlStart.y;
                right = Mathf.Lerp(girlStart.z, girlStart.z + 250f, t);
                top = girlStart.w;
            }

            float left2;
            float bottom2;
            float right2;
            float top2;

            if (!isExpanded)
            {
                left2 = Mathf.Lerp(garbageStart.x, garbageStart.x + 250f, t);
                bottom2 = garbageStart.y;
                right2 = Mathf.Lerp(garbageStart.z, garbageStart.z + 250f, t);
                top2 = garbageStart.w;
            }
            else
            {
                left2 = Mathf.Lerp(garbageStart.x, garbageStart.x - 250f, t);
                bottom2 = garbageStart.y;
                right2 = Mathf.Lerp(garbageStart.z, garbageStart.z - 250f, t);
                top2 = garbageStart.w;
            }

            girlHp.offsetMin = new Vector2(left, bottom);
            girlHp.offsetMax = new Vector2(right, top);

            garbageHp.offsetMin = new Vector2(left2, bottom2);
            garbageHp.offsetMax = new Vector2(right2, top2);

            yield return null;
        }

        if (!isExpanded)
        {
            girlHp.offsetMin = new Vector2(girlStart.x - 250f, girlStart.y);
            girlHp.offsetMax = new Vector2(girlStart.z - 250f, girlStart.w);
        }
        else
        {
            girlHp.offsetMin = new Vector2(girlStart.x + 250f, girlStart.y);
            girlHp.offsetMax = new Vector2(girlStart.z + 250f, girlStart.w);
        }
        
        if (isExpanded)
        {
            garbageHp.offsetMin = new Vector2(garbageStart.x - 250f, garbageStart.y);
            garbageHp.offsetMax = new Vector2(garbageStart.z - 250f, garbageStart.w);
        }
        else
        {
            garbageHp.offsetMin = new Vector2(garbageStart.x + 250f, garbageStart.y);
            garbageHp.offsetMax = new Vector2(garbageStart.z + 250f, garbageStart.w);   
        }
    }

    public RectTransform GetButtons()
    {
        return buttons;
    }
}
