using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 offset;
    [SerializeField] private float lerpTime;


    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(transform.position, offset + playerTransform.position, lerpTime * Time.deltaTime);
        transform.position = newPos;
    }
}
