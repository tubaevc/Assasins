using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaypointNavigator : MonoBehaviour
{
    private CharacterNavigator _characterNavigator;
    [SerializeField] public Waypoint currentWaypoint;
    private int direction;

    private void Awake()
    {
        _characterNavigator = GetComponent<CharacterNavigator>();
    }

    private void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        _characterNavigator.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (_characterNavigator.destinationReached)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.prevWaypoint; //opposite direction
            }

            _characterNavigator.LocateDestination(currentWaypoint.GetPosition());
        }
    }
}