using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllysPatrol : MonoBehaviour
{
    [SerializeField] private List<PatrolPoint> _patrolPoints;

    public static AllysPatrol Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public Transform GetEmptyPoint(SoldierAttack soldier)
    {
        PatrolPoint patrol = soldier.currentPatrolPoint;
        foreach (var item in _patrolPoints)
        {
            if (item.IsEmpty)
            {
                patrol.IsEmpty = false;
                patrol = item;
                patrol.IsEmpty = true;
                soldier.currentPatrolPoint = patrol;
                break;
            }
        }
        return patrol.Point;
    }


    
}

[Serializable]
public class PatrolPoint
{
    public bool IsEmpty;
    public Transform Point;
}
