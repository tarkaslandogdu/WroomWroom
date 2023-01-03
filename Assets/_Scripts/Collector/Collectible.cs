using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] ParticleSystem smokePuff;
    [SerializeField] float destroyTime = 5f;
    [SerializeField] AudioClip poofs;
    [SerializeField] AudioSource audioSource;


    public void PlayParticle()
    {
        audioSource.PlayOneShot(poofs);
        smokePuff.Play();
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("Collected");
        }
    }
}
