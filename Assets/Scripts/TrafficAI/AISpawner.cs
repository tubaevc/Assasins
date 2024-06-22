using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] AIPrefab;
    [SerializeField] private int AiToSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count<AiToSpawn)
        {
            int randomIndex = Random.Range(0, AIPrefab.Length);
            GameObject obj = Instantiate(AIPrefab[randomIndex]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            yield return new WaitForSeconds(1f);
            count++;
        }
    }
}
