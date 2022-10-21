using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    float horizontalInput;
    [SerializeField] float horizontalSpeed = 2f;
    
    [SerializeField] Rigidbody rb;
    [SerializeField] Collector collector;

    bool alive = true;
    [SerializeField] GameObject bulletPoint;

    void Update()
    {
        Movement();
        ProccessShoting();
    }

    void Movement()
    {
        if (!alive) return;

        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > LevelBoundary.leftSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < LevelBoundary.rightSide)
            {
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed * -1);
            }
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

    void ProccessShoting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bulletPoint.GetComponent<ShotBullet>().Fire();

        }
    }
}
