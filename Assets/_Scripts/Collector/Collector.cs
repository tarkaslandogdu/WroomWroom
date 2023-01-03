using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Collector : MonoBehaviour
{
    public List<GameObject> boxes = new();

    [Header("Objects and Text")]
    [SerializeField] GameObject boxPrefab;
    [SerializeField] GameObject holderIndex;
    Transform boxHolderTransform;
    PlayerMovement player;
    Finish finish;

    [Header("Veriables")]
    [SerializeField] float boxSpawnDespawnSpeed = 1f;
    [SerializeField] float boxSpawnScale = 1.5f;
    [SerializeField] float boxScaleTimer = 0.1f;
    [SerializeField] float scatherRadius = 1f;
    [SerializeField] AudioClip pickUp;
    [SerializeField] AudioClip endingSounds;
    AudioSource playerSource;

    [SerializeField] TMP_Text boxCountTex = null;

    int gateNumber, gateType, boxToRemove, boxtToAddMultiply, boxToRemoveDivide, boxtafterMultiply;

    void Start()
    {
        holderIndex.transform.GetChild(PlayerPrefs.GetInt("playerindex")).transform.gameObject.SetActive(true);
        boxHolderTransform = holderIndex.transform.GetChild(PlayerPrefs.GetInt("playerindex")).transform;
        boxCountTex = boxHolderTransform.GetChild(0).GetComponent<TextMeshPro>();
        player = GetComponent<PlayerMovement>();
        finish = FindObjectOfType<Finish>();
        playerSource = GetComponent<AudioSource>();
        
    }

    void Update() => boxCountTex.text = boxes.Count.ToString();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Collectible>())
        {
            if (boxes.Count == 0)
            {
                other.transform.SetParent(boxHolderTransform);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.transform.localPosition = new Vector3(0, 0, 0);

                other.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                other.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                other.transform.localRotation = Quaternion.identity;
                boxes.Add(other.gameObject);
            }

            else if (boxes.Count >= 1)
            {
                other.transform.SetParent(boxHolderTransform);
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                other.transform.localPosition = new Vector3(0, boxes[^1].transform.localPosition.y + .4f, 0);

                other.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                other.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                other.transform.localRotation = Quaternion.identity;
                boxes.Add(other.gameObject);
            }
        }

        if (other.gameObject.GetComponent<GateController>())
        {
            gateType = other.gameObject.GetComponent<GateController>().gateType;
            gateNumber = other.gameObject.GetComponent<GateController>().gateNumber;

            boxToRemove = boxes.Count + gateNumber;
            boxtToAddMultiply = boxes.Count * gateNumber;
            boxtafterMultiply = boxtToAddMultiply - boxes.Count;
            boxToRemoveDivide = boxes.Count / gateNumber;

            if (gateType == 1)
            {
                StartCoroutine(PositiveBoxCount(boxSpawnDespawnSpeed));
                Debug.Log(gateNumber + " + ");
            }
            if (gateType == 2)
            {
                StartCoroutine(NegativeBoxCount(boxSpawnDespawnSpeed));
                Debug.Log(boxToRemove + " - ");
            }
            if (gateType == 3)
            {
                StartCoroutine(MultiplyerBoxCount(boxSpawnDespawnSpeed));
                Debug.Log(boxtToAddMultiply + " * ");
            }
            if (gateType == 4)
            {
                StartCoroutine(DivideBoxCount(boxSpawnDespawnSpeed));
                Debug.Log(boxToRemoveDivide + " / ");
            }
        }

        if (other.GetComponent<Obstacle>())
        {
            for (int i = boxes.Count - 1; i >= 0; i--)
            {
                ScatherBoxes(boxes[i]);
                boxes.RemoveAt(i);
            }
        }
    }

    IEnumerator PositiveBoxCount(float delay)
    {
        for (int i = 0; i < gateNumber; i++)
        {
            GameObject newBoxToAdd = Instantiate(boxPrefab);
            if (boxes.Count >= 1)
            {
                newBoxToAdd.transform.SetParent(boxHolderTransform);
                newBoxToAdd.GetComponent<BoxCollider>().enabled = false;
                newBoxToAdd.transform.localPosition = new Vector3(0, boxes[^1].transform.localPosition.y + .4f, 0);

                newBoxToAdd.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                newBoxToAdd.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                newBoxToAdd.transform.localRotation = Quaternion.identity;
                boxes.Add(newBoxToAdd);
            }
            else if(boxes.Count == 0)
            {
                newBoxToAdd.transform.SetParent(boxHolderTransform);
                newBoxToAdd.GetComponent<BoxCollider>().enabled = false;
                newBoxToAdd.transform.localPosition = new Vector3(0, 0, 0);

                newBoxToAdd.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                newBoxToAdd.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                newBoxToAdd.transform.localRotation = Quaternion.identity;
                boxes.Add(newBoxToAdd);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator NegativeBoxCount(float delay)
    {
        for (int i = boxes.Count - 1; i >= boxToRemove; i--)
        {
            if (i < 0) yield break;
            ScatherBoxes(boxes[i]);
            boxes.RemoveAt(i);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator MultiplyerBoxCount(float delay)
    {
        for (int i = 0; i < boxtafterMultiply; i++)
        {
            GameObject newBoxToAdd = Instantiate(boxPrefab);
            if (boxes.Count >= 1)
            {
                newBoxToAdd.transform.SetParent(boxHolderTransform);
                newBoxToAdd.GetComponent<BoxCollider>().enabled = false;
                newBoxToAdd.transform.localPosition = new Vector3(0, boxes[^1].transform.localPosition.y + .4f, 0);

                newBoxToAdd.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                newBoxToAdd.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                newBoxToAdd.transform.localRotation = Quaternion.identity;
                boxes.Add(newBoxToAdd);
            }
            else if(boxes.Count == 0)
            {
                newBoxToAdd.transform.SetParent(boxHolderTransform);
                newBoxToAdd.GetComponent<BoxCollider>().enabled = false;
                newBoxToAdd.transform.localPosition = new Vector3(0, 0, 0);

                newBoxToAdd.transform.DOScale(Vector3.one * boxSpawnScale, boxScaleTimer).OnComplete(() =>
                newBoxToAdd.transform.DOScale(Vector3.one, boxScaleTimer));
                playerSource.PlayOneShot(pickUp);

                newBoxToAdd.transform.localRotation = Quaternion.identity;
                boxes.Add(newBoxToAdd);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator DivideBoxCount(float delay)
    {
        for (int i = boxes.Count - 1; i >= boxToRemoveDivide; i--)
        {
            if (i < 0) yield break;
            ScatherBoxes(boxes[i]);
            boxes.RemoveAt(i);
            yield return new WaitForSeconds(delay);
        }
    }

    void ScatherBoxes(GameObject missedBox)
    {
        Vector3 lastBoxPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 2);
        Vector3 jumpPosition = lastBoxPos + Random.insideUnitSphere * scatherRadius;

        missedBox.GetComponent<Collectible>().PlayParticle();
        missedBox.transform.parent = null;
        missedBox.transform.DOJump(jumpPosition, 1f, 1, 0.5f).OnComplete(() => missedBox.GetComponent<BoxCollider>().enabled = true);
        missedBox.GetComponent<BoxCollider>().isTrigger = false;
        missedBox.AddComponent<Rigidbody>();

    }

    public IEnumerator FinishSeq(float delay)
    {
        if (player.finished)
        {
            if (boxes.Count == 0) finish.EndingCanvasFade();
            int coins = PlayerPrefs.GetInt("coins");
            GameObject end = GameObject.FindGameObjectWithTag("End");
            for (int i = boxes.Count - 1; i >= 0; i--)
            {
                if (i <= 0)
                {
                    finish.EndingCanvasFade();
                }
                boxes[i].transform.DOMove(end.transform.position, 1);
                PlayerPrefs.SetInt("coins", coins += 1);
                playerSource.PlayOneShot(endingSounds);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}