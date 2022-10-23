using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collector : MonoBehaviour
{
    [SerializeField] List<GameObject> boxes = new();
    [Header("Objects and Text")]
    [SerializeField] GameObject boxPrefab;
    [SerializeField] Transform boxHolderTransform;

    [SerializeField] TMP_Text boxCountTex = null;

    int gateNumber;
    int targetBoxCount;

    void Update()
    {
        UptadeBoxCountText();
    }

    void UptadeBoxCountText()
    {
        boxCountTex.text = boxes.Count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Collect"))
        {
            other.transform.SetParent(boxHolderTransform);
            other.GetComponent<BoxCollider>().enabled = false;
            other.transform.localPosition = new Vector3(0, boxes[^1].transform.localPosition.y + .4f, 0);
            other.transform.localScale = new Vector3(1, 1, 1);
            other.transform.localRotation = Quaternion.identity;
            boxes.Add(other.gameObject);
        }

        if (other.CompareTag("Gate"))
        {
            gateNumber = other.gameObject.GetComponent<GateController>().BringGateNumber();
            targetBoxCount = boxes.Count + gateNumber;

            if (gateNumber > 0)
            {
                PositiveBoxCount();
            }
            else if (gateNumber < 0)
            {
                NegativeBoxCount();
            }
        }
    }

    void PositiveBoxCount()
    {
        for (int i = 0; i < gateNumber; i++)
        {
            GameObject newBoxToAdd = Instantiate(boxPrefab);
            newBoxToAdd.transform.SetParent(boxHolderTransform);
            newBoxToAdd.GetComponent<BoxCollider>().enabled = false;
            newBoxToAdd.transform.localPosition = new Vector3(0, boxes[^1].transform.localPosition.y + .4f, 0);
            newBoxToAdd.transform.localScale = new Vector3(1, 1, 1);
            newBoxToAdd.transform.localRotation = Quaternion.identity;
            boxes.Add(newBoxToAdd);
        }
    }
    
    void NegativeBoxCount()
    {
        for (int i = boxes.Count - 1; i >= targetBoxCount; i--)
        {
            boxes.RemoveAt(i);
        }
    }
}