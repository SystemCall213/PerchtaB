using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public PanelExpander_Anchors battleField;
    public Enemy enemy;
    public RectTransform buttons;
    public float animationDuration = 2f;

    public void Toggle()
    {
        battleField.TogglePanel();
        StartCoroutine(moveEnemy());
        StartCoroutine(moveButtons());
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

            float left = start.x;
            float bottom = Mathf.Lerp(start.y, start.y + 600f, t);
            float right = start.z;
            float top = Mathf.Lerp(start.w, start.w + 600f, t); // move up 600

            enemyTransform.offsetMin = new Vector2(left, bottom);
            enemyTransform.offsetMax = new Vector2(right, top);

            yield return null;
        }

        enemyTransform.offsetMin = new Vector2(start.x, start.y + 600f);
        enemyTransform.offsetMax = new Vector2(start.z, start.w + 600f);

        enemy.Execute();
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

            float left = start.x;
            float bottom = Mathf.Lerp(start.y, start.y - 300f, t); // move down 300
            float right = start.z;
            float top = Mathf.Lerp(start.w, start.w - 300f, t);;

            buttons.offsetMin = new Vector2(left, bottom);
            buttons.offsetMax = new Vector2(right, top);

            yield return null;
        }

        buttons.offsetMin = new Vector2(start.x, start.y - 300f);
        buttons.offsetMax = new Vector2(start.z, start.w - 300f);
    }

}
