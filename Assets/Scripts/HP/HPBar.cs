using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public HPPoint hPPointPrefab;
    public int maxHp;
    private List<HPPoint> hPPoints;
    public Sprite hpPointSprite;

    // Queue for sequential processing
    private readonly Queue<HPPoint> damageQueue = new Queue<HPPoint>();
    private bool isProcessingDamage = false;

    // Animation parameters
    [SerializeField] private float hpShrinkDuration = 0.25f;
    [SerializeField] private float delayBetweenHp = 0.1f; // time between each HP animation

    private void Awake()
    {
        hPPoints = new List<HPPoint>();
        Populate(maxHp);
    }

    public int TakeDmg(int numOfDmg)
    {
        if (CurrentHp() > 0)
        {
            for (int i = 0; i < numOfDmg; i++)
            {
                if (hPPoints.Count == 0) break;

                // Remove from the HP list immediately so counts are correct
                HPPoint hpPoint = hPPoints[hPPoints.Count - 1];
                hPPoints.RemoveAt(hPPoints.Count - 1);

                // Enqueue for sequential animation + destruction
                damageQueue.Enqueue(hpPoint);
            }

            // Start processing queue if not already running
            if (!isProcessingDamage)
                StartCoroutine(ProcessDamageQueue());
        }

        return hPPoints.Count;
    }

    private IEnumerator ProcessDamageQueue()
    {
        isProcessingDamage = true;

        while (damageQueue.Count > 0)
        {
            HPPoint hp = damageQueue.Dequeue();

            // Animate shrink then kill
            yield return StartCoroutine(AnimateHpLoss(hp));

            // optional small delay between items
            yield return new WaitForSeconds(delayBetweenHp);
        }

        isProcessingDamage = false;
    }

    private IEnumerator AnimateHpGain(HPPoint hp)
    {
        Transform t = hp.transform;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;  // Assuming original scale is (1,1,1)

        float time = 0f;

        while (time < hpShrinkDuration)
        {
            time += Time.deltaTime;
            float t01 = Mathf.Clamp01(time / hpShrinkDuration);
            t.localScale = Vector3.Lerp(startScale, endScale, t01);
            yield return null;
        }

        t.localScale = endScale;
    }

    private IEnumerator AnimateHpLoss(HPPoint hp)
    {
        // If HPPoint uses a transform scale animation:
        Transform t = hp.transform;
        Vector3 startScale = t.localScale;
        Vector3 endScale = Vector3.zero;

        float time = 0f;

        while (time < hpShrinkDuration)
        {
            time += Time.deltaTime;
            float t01 = Mathf.Clamp01(time / hpShrinkDuration);
            t.localScale = Vector3.Lerp(startScale, endScale, t01);
            yield return null;
        }

        t.localScale = endScale;

        // Finally call Kill() (your method that handles removal/destruction)
        hp.Kill();
    }


    public void Heal(int healAmount)
    {
        Populate(healAmount);
    }
    
    public void Populate(int hpNum)
    {
        for (int i = 0; i < hpNum; i++)
        {
            if (hPPoints.Count < maxHp)
            {
                HPPoint hPPoint = Instantiate(
                    hPPointPrefab,
                    transform.position,
                    Quaternion.identity,
                    transform
                );

                hPPoint.sprite = hpPointSprite;

                // Add to list BEFORE animating (so CurrentHp is correct)
                hPPoints.Add(hPPoint);

                // Start at zero scale (invisible)
                hPPoint.transform.localScale = Vector3.zero;
                hPPoint.transform.localPosition = Vector3.zero;

                // Animate appearing
                StartCoroutine(AnimateHpGain(hPPoint));
            }
        }
    }

    public int CurrentHp()
    {
        return hPPoints.Count;
    }

    public int MaxHp()
    {
        return maxHp;
    }
}
