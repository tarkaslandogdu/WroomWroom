using UnityEngine;

public class BulletScipt : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
