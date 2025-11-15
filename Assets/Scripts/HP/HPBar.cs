using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public HPPoint hPPointPrefab;
    public int numOfHp;

    private List<HPPoint> hPPoints;

    private void Start()
    {
        hPPoints = new List<HPPoint>();
        for (int i = 0; i < numOfHp; i++)
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
    
    public void takeDmg(int numOfDmg)
    {
        for (int i = 0; i < numOfDmg; i++)
        {
            HPPoint hPPoint = hPPoints.Last();
            hPPoints.RemoveAt(hPPoints.Count - 1);

            hPPoint.Kill();
        }
    }
}
