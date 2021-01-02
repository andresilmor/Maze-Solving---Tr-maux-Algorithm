using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("Waypoint Config:")]
    [SerializeField] Waypoint[] waypointConnected;
    [SerializeField] bool goal;
    [SerializeField] byte state = 0; // 0 - Não visitado / 1 - Visitado uma vez / Visitado duas vezes
    public bool hasNoWayOut;

    private void Start()
    {
        hasNoWayOut = false;
    }

    public Waypoint[] GetWaypointsConnected()
    {
        return waypointConnected;
    }

    public void MarkWaypoint()
    {
        if (state < 2)
        {
            state++;
        }
    }

    public void DesmarkWaypoint() //Apenas usado em situações de beco
    {
        if (state > 0)
        {
            state--;
        }
    }

    public int GetState()
    {
        return state;
    }

    public bool IsGoal()
    {
        return goal;
    }

    public void NoWayOut()
    {
        hasNoWayOut = true;
    }

    public bool HasNoWayOut()
    {
        return hasNoWayOut;
    }
}
