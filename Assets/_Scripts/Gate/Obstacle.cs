using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            StartCoroutine(CrashSeq(other.GetComponent<PlayerMovement>()));
        }
    }

    IEnumerator CrashSeq(PlayerMovement playerMovement)
    {
        playerMovement.crashed = true;
        playerMovement.GetComponent<Animator>().SetBool("Crash", true);
        yield return new WaitForSeconds(waitTime);
        Debug.Log("deneme");
        playerMovement.crashed = false;
        playerMovement.GetComponent<Animator>().SetBool("Crash", false);
        Destroy(gameObject, 0.1f);
        yield break;
    }
}
