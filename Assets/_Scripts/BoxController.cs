using UnityEngine;

public class BoxController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
