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
                Player.Instance.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                Player.Instance.GetComponent<SpriteRenderer>().enabled = true;
            }
            BattleText.Instance.SetText("");
            battleField.TogglePanel();
            StartCoroutine(moveEnemy());
            StartCoroutine(moveButtons());
            StartCoroutine(moveHpBars());
            if (!isExpanded) enemy.Execute();
            isExpanded = !isExpanded;   
        }
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
                bottom = Mathf.Lerp(start.y, start.y - 300f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w - 300f, t);
            }
            else
            {
                left = start.x;
                bottom = Mathf.Lerp(start.y, start.y + 300f, t);
                right = start.z;
                top = Mathf.Lerp(start.w, start.w + 300f, t);
            }

            buttons.offsetMin = new Vector2(left, bottom);
            buttons.offsetMax = new Vector2(right, top);

            yield return null;
        }

        if (isExpanded)
        {
            buttons.offsetMin = new Vector2(start.x, start.y - 300f);
            buttons.offsetMax = new Vector2(start.z, start.w - 300f);
        }
        else
        {
            buttons.offsetMin = new Vector2(start.x, start.y + 300f);
            buttons.offsetMax = new Vector2(start.z, start.w + 300f);
        }
    }

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
