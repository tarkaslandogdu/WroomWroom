using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] TMP_Text  gateNumberText = null;

    //[SerializeField] enum GateType { positiveGate, negativeGate, multiplyerGate, dividerGate }
    //[SerializeField] GateType gateType;
    [SerializeField] public int gateType;
    [SerializeField] public int gateNumber;

    [SerializeField, Range(0, 10)] int maxPositiveNumber = 10;
    [SerializeField, Range(-10, 0)] int maxNegativeNumber = -10;
    [SerializeField, Range(2, 5)] int maxMultiplyerNumber = 5;
    [SerializeField, Range(2, 5)]int maxDividerNumber = 5;

    int positiveGateNumber;
    int negativeGateNumber;
    int multiplyerGateNumber;
    int dividerGateNumber;

    public int BringGateType() { return gateType; }
    public int BringGateNumber() { return gateNumber; }

    
    void Start()
    {
        RandomGateType();
        //GetRandomNuberForDoors();
        GateNumberRandomizer();

        Invoke(nameof(LateRandm), 1);
    }

    void LateRandm()
    {
        if (PlayerPrefs.GetInt("new") == 0)
        {
            PlayerPrefs.SetInt("new", 1);
        }
    }

    void GateNumberRandomizer()
    {
        switch (gateType)
        {
            case 1:
                gateNumber = Random.Range(1, maxPositiveNumber);  //positiveGateNumber;
                gateNumberText.text = "+" + gateNumber.ToString();
                break;
            case 2:
                gateNumber = Random.Range(maxNegativeNumber, -1); //negativeGateNumber;
                gateNumberText.text = gateNumber.ToString();
                break;
            case 3:
                gateNumber = Random.Range(2, maxMultiplyerNumber); //multiplyerGateNumber;
                gateNumberText.text = "x" + gateNumber.ToString();
                break;
            case 4:
                gateNumber = Random.Range(2, maxDividerNumber); //dividerGateNumber;
                gateNumberText.text = "÷" + gateNumber.ToString();
                break;
        }
    }

    void RandomGateType()
    {
        if (PlayerPrefs.GetInt("new" + this.transform.name) == 0)
        {
            gateType = Random.Range(1, 5);
            string gateTypeName = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name
                                + gameObject.transform.parent.transform.parent.name + "dortype";
            PlayerPrefs.SetInt(gateTypeName, gateType);
        }
        else
        {
            string gateTypeName = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name
                                + gameObject.transform.parent.transform.parent.name + "dortype";

            gateType = PlayerPrefs.GetInt(gateTypeName);
        }
    }

    //void GetRandomNuberForDoors()
    //{
    //    if (PlayerPrefs.GetInt("new" + this.transform.name) == 0)
    //    {
    //        string gateTypeName = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name
    //                               + gameObject.transform.parent.transform.parent.name + "number";

    //        if (gateType == 1)
    //        {
    //            positiveGateNumber = Random.Range(1, maxPositiveNumber);
    //            PlayerPrefs.SetInt(gateTypeName, positiveGateNumber);
    //        }
    //        if (gateType == 2)
    //        {
    //            negativeGateNumber = Random.Range(maxNegativeNumber, -1);
    //            PlayerPrefs.SetInt(gateTypeName, negativeGateNumber);

    //        }
    //        if (gateType == 3)
    //        {
    //            multiplyerGateNumber = Random.Range(2, maxMultiplyerNumber);
    //            PlayerPrefs.SetInt(gateTypeName, multiplyerGateNumber);
    //        }
    //        if (gateType == 4)
    //        {
    //            dividerGateNumber = Random.Range(2, maxDividerNumber);
    //            PlayerPrefs.SetInt(gateTypeName, dividerGateNumber);
    //        }
    //    }
    //    else
    //    {
    //        string gateTypeName = gameObject.transform.parent.transform.parent.transform.parent.transform.parent.name
    //                            + gameObject.transform.parent.transform.parent.name + "number";

    //        if (gateType == 1)
    //        {
    //            positiveGateNumber = PlayerPrefs.GetInt(gateTypeName);
    //        }
    //        if (gateType == 2)
    //        {
    //            negativeGateNumber = PlayerPrefs.GetInt(gateTypeName);

    //        }
    //        if (gateType == 3)
    //        {
    //            multiplyerGateNumber = PlayerPrefs.GetInt(gateTypeName);
    //        }
    //        if (gateType == 4)
    //        {
    //            dividerGateNumber = PlayerPrefs.GetInt(gateTypeName);
    //        }
    //    }
    //}
}
