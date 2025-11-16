using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public HPPoint hPPointPrefab;
    public int maxHp;
    private List<HPPoint> hPPoints;

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
                HPPoint hPPoint = hPPoints.Last();
                hPPoints.RemoveAt(hPPoints.Count - 1);

                hPPoint.Kill();
            }    
        }
        return hPPoints.Count;
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

                hPPoint.transform.localPosition = Vector3.zero;

                hPPoints.Add(hPPoint);   
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
