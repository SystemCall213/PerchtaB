using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    protected Transform target;
    private int numOfTries;
    private int minNumOfTries = 7;
    private int maxNumOfTries = 10;


    private void Awake()
    {
        numOfTries = Random.Range(minNumOfTries, maxNumOfTries);
    }

    public void SetPlayer(Transform _playerPos)
    {
        target = _playerPos;
    }
}
