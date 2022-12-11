using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] float lerpTime;


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
