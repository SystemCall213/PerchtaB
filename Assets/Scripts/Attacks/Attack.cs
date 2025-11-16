using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public List<AttackArea> attackAreas;

    public abstract void Execute();
}
