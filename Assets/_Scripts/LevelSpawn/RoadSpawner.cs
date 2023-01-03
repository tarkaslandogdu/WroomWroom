using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [Header("Road")]
    [SerializeField] float roadCount;
    [SerializeField] GameObject road;
    [SerializeField] GameObject endRoad;
    Vector3 roadSpawner;

    void Awake()
    {
        if (PlayerPrefs.GetInt("level") == 0) PlayerPrefs.SetInt("level", 1);
        SpawnTile(); 
    }

    void SpawnTile()
    {
        for (int i = 0; i < roadCount; i++)
        {
            if (i < roadCount - 1)
            {
                GameObject roadNew = Instantiate(road, roadSpawner, Quaternion.identity);
                roadNew.transform.name = roadNew.transform.name + i;
                roadSpawner = roadNew.transform.GetChild(0).transform.position;
                roadNew.transform.parent = gameObject.transform;
            }
            if (i == roadCount - 1)
            {
                GameObject ending = Instantiate(endRoad, roadSpawner, Quaternion.identity);
                ending.transform.parent = gameObject.transform;
            }
        }
    }
}
