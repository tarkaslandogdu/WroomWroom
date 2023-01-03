using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoom : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    void Update() => transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
}
