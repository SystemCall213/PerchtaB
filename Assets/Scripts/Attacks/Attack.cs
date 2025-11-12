using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public List<AttackArea> attackAreas;

    public void Execute()
    {
        foreach (AttackArea area in attackAreas)
        {
            area.Activate();
        }
    }
}
