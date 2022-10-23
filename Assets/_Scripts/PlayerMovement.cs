using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    [SerializeField] float horizontalSpeed = 2f;

    [Header("")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Collector collector;
    [SerializeField] GameObject bulletPoint;

    [Header("LevelBoundary")]
    [SerializeField] float leftSide = -2.5f;
    [SerializeField] float rightSide = 2.5f;

    bool alive = true;

    void Update()
    {
        Movement();
        Shoting();
    }

    void Movement()
    {
        if (!alive) return;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > leftSide)
            {
                transform.Translate(horizontalSpeed * Time.deltaTime * Vector3.left);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < rightSide)
            {
                transform.Translate(-1 * horizontalSpeed * Time.deltaTime * Vector3.left);
            }
        }

    }

    void Shoting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletPoint.GetComponent<ShotBullet>().Fire();

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    void Die()
    {
        alive = false;
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
