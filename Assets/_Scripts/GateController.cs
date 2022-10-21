using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateController : MonoBehaviour
{
    [SerializeField] TMP_Text  gateNumberText = null;
    [SerializeField] enum GateType { positiveGate, negativeGate }

    [SerializeField] GateType gateType;
    [SerializeField] int gateNumber;

    [SerializeField]int maxPositiveNumber = 10;
    [SerializeField]int maxNegativeNumber = -10;

    public int BringGateNumber()
    {
        return gateNumber;
    }

    void Start()
    {
        GateNumberRandomizer();
    }

    void GateNumberRandomizer()
    {
        switch (gateType)
        {
            case GateType.positiveGate :
                gateNumber = Random.Range(1, maxPositiveNumber);
                gateNumberText.text = gateNumber.ToString();
                break;
            case GateType.negativeGate :
                gateNumber = Random.Range(maxNegativeNumber, -1);
                gateNumberText.text = gateNumber.ToString();
                break;
        }
    }
}
