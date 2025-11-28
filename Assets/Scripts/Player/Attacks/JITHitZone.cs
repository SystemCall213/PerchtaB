using System;
using UnityEngine;

public class JITHitZone : MonoBehaviour
{
    public Action<int> inTheZone;
    public int enterDmg;
    public int exitDmg;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        inTheZone.Invoke(enterDmg);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inTheZone.Invoke(exitDmg);
    }
}
