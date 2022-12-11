using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    [SerializeField] GameObject boxPrefab;

    void Start()
    {
        SpawnBoxes();
    }

    public void SpawnBoxes()
    {
        int boxToSpawn = 20;

        for (int i = 0; i < boxToSpawn; i++)
        {
            GameObject temp = Instantiate(boxPrefab);
            temp.transform.position = GetRandomPointInColider(GetComponent<Collider>());
        }

        Vector3 GetRandomPointInColider(Collider collider)
        {
            Vector3 point = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z));

            if (point != collider.ClosestPoint(point))
            {
                point = GetRandomPointInColider(collider);
            }

            point.y = 0.04f;
            return point;
        }
    }
}