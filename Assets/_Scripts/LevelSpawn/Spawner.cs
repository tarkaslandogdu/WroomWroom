using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Hauses")]
    [SerializeField] GameObject[] hauses;
    [SerializeField] Transform[] hauseLocations;

    [Header("Door")]
    [SerializeField] GameObject door;
    [SerializeField] Transform[] doorLocations;

    [Header("Barier")]
    [SerializeField] GameObject barier;
    [SerializeField] Transform[] barierLocations;

    [Header("Boxes")]
    [SerializeField] GameObject boxes;
    [SerializeField] Transform[] boxLocations;

    //Randoms
    int hauseRandom;
    int doorRandom;
    Vector3 randomDoorPos;
    int barierRandom;
    int barierRandomRange;
    Vector3 randomBarierPos;
    int boxRandom;
    int boxPos;
    int boxCount;
    Vector3 boxPosition;


    void Start()
    {
        HauseSpawn();
        DoorSpawn();
        BarierSpawn();
        BoxSpawn();

        Invoke(nameof(LateRandım), 1);

    }

    private void LateRandım()
    {
        if (PlayerPrefs.GetInt("new") == 0)
        {
            PlayerPrefs.SetInt("new", 1);
        }
    }

    void HauseSpawn()
    {
        for (int i = 0; i < hauseLocations.Length; i++)
        {
            if (PlayerPrefs.GetInt("new") == 0)
            {
                hauseRandom = Random.Range(0, hauses.Length);
                GameObject newhause = Instantiate(hauses[hauseRandom], hauseLocations[i].position, hauseLocations[i].rotation);
                newhause.transform.parent = hauseLocations[i].transform;

                string hauseSaveName = hauseLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                     + hauseLocations[i].gameObject.name;
                PlayerPrefs.SetInt(hauseSaveName, hauseRandom);
            }
            else
            {
                string hauseSaveName = hauseLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                     + hauseLocations[i].gameObject.name;

                hauseRandom = PlayerPrefs.GetInt(hauseSaveName);
                GameObject newhause = Instantiate(hauses[hauseRandom], hauseLocations[i].position, hauseLocations[i].rotation);
                newhause.transform.parent = hauseLocations[i].transform;

            }

        }

    }

    void DoorSpawn()
    {
        doorRandom = Random.Range(-5, 5);
        for (int i = 0; i < doorLocations.Length; i++)
        {
            if (PlayerPrefs.GetInt("new") == 0)
            {
                randomDoorPos = new Vector3(doorLocations[i].position.x,
                                            doorLocations[i].position.y,
                                            doorLocations[i].position.z + doorRandom);

                GameObject newdoor = Instantiate(door, randomDoorPos, doorLocations[i].rotation);
                newdoor.transform.parent = doorLocations[i].transform;
                newdoor.transform.gameObject.name = newdoor.transform.name + i;

                string doorSaveName = doorLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                    + doorLocations[i].gameObject.name;
                PlayerPrefs.SetInt(doorSaveName, doorRandom);
            }
            else
            {
                string doorSaveName = doorLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                    + doorLocations[i].gameObject.name;
                int doorRandomSave = PlayerPrefs.GetInt(doorSaveName);

                randomDoorPos = new Vector3(doorLocations[i].position.x,
                                            doorLocations[i].position.y,
                                            doorLocations[i].position.z + doorRandomSave);

                GameObject newdoor = Instantiate(door, randomDoorPos, doorLocations[i].rotation);
                newdoor.transform.parent = doorLocations[i].transform;


            }

        }
    }

    void BarierSpawn()
    {
        if (PlayerPrefs.GetInt("new") == 0)
        {
            barierRandom = Random.Range(0, barierLocations.Length + 1);
            string barierRange = gameObject.gameObject.name + "barier";
            PlayerPrefs.SetInt(barierRange, barierRandom);
        }
        else
        {
            string barierRange = gameObject.gameObject.name + "barier";
            barierRandom = PlayerPrefs.GetInt(barierRange);

        }

        for (int i = 0; i < barierRandom; i++)
        {
            if (PlayerPrefs.GetInt("new") == 0)
            {
                barierRandomRange = Random.Range(0, 10);
                randomBarierPos = new Vector3(barierLocations[i].position.x,
                                              barierLocations[i].position.y,
                                              barierLocations[i].position.z + barierRandomRange);

                GameObject barierNew = Instantiate(barier, randomBarierPos, barierLocations[i].rotation);
                barierNew.transform.parent = barierLocations[i].transform;

                string barierSaveName = barierLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                      + barierLocations[i].gameObject.name;
                PlayerPrefs.SetInt(barierSaveName, barierRandomRange);
            }
            else
            {
                string barierSaveName = barierLocations[i].gameObject.transform.parent.transform.parent.transform.gameObject.name
                                      + barierLocations[i].gameObject.name;
                barierRandomRange = PlayerPrefs.GetInt(barierSaveName);

                randomBarierPos = new Vector3(barierLocations[i].position.x,
                                              barierLocations[i].position.y,
                                              barierLocations[i].position.z + barierRandomRange);

                GameObject barierNew = Instantiate(barier, randomBarierPos, barierLocations[i].rotation);
                barierNew.transform.parent = barierLocations[i].transform;
            }

        }
    }

    void BoxSpawn()
    {
        if (PlayerPrefs.GetInt("new") == 0)
        {
            boxRandom = Random.Range(1, boxLocations.Length);
            string boxRange = gameObject.name + "box";
            PlayerPrefs.SetInt(boxRange, boxRandom);
        }
        else
        {
            string boxRange = gameObject.name + "box";
            boxRandom = PlayerPrefs.GetInt(boxRange);
        }

        for (int a = 0; a < boxRandom; a++)
        {
            if (PlayerPrefs.GetInt("new") == 0)
            {
                boxPos = Random.Range(0, boxLocations.Length);
                string boxSaveName = gameObject.name + "boxpos";
                PlayerPrefs.SetInt(boxSaveName, boxPos);
            }
            else
            {
                string boxSaveName = gameObject.name + "boxpos";
                boxPos = PlayerPrefs.GetInt(boxSaveName);
            }

            if (PlayerPrefs.GetInt("new" + PlayerPrefs.GetInt("level")) == 0)
            {
                boxCount = Random.Range(4, 10);
                string boxCountName = gameObject.name + "count";
                PlayerPrefs.SetInt(boxCountName, boxCount);
            }
            else
            {
                string boxCountName = gameObject.name + "count";
                boxCount = PlayerPrefs.GetInt(boxCountName);
            }
            int point = 0;
            for (int i = 0; i < boxCount; i++)
            {
                boxPosition = new Vector3(boxLocations[boxPos].position.x,
                                                boxLocations[boxPos].position.y,
                                                boxLocations[boxPos].position.z + point);
                GameObject boxNew = Instantiate(boxes, boxPosition, boxLocations[boxPos].rotation);

                boxNew.transform.parent = boxLocations[boxPos].transform;
                point += 1;
            }
        }
    }
}
