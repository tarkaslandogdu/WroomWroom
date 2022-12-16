using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    [SerializeField] float horizontalSpeed = 2f;
    [SerializeField] float turnSpeed = 8f;

    [Header("")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Collector collector;
    [SerializeField] GameObject bulletPoint;
    [SerializeField] ParticleSystem smoke;

    [Header("LevelBoundary")]
    [SerializeField] float leftSide = -2.5f;
    [SerializeField] float rightSide = 2.5f;

    [Header("")]
    public bool alive = true;
    public bool gameRuning = false;
    public bool finished = false;

    GameObject canvas;

    void Update()
    {
        Movement();
        Shoting();
        canvas = GameObject.FindGameObjectWithTag("Ui");
    }

    void Movement()
    {
        if (finished) return;
        if (!alive || !gameRuning) return;
        float turnVector = Input.GetAxis("Horizontal") * turnSpeed;

        transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward, Space.World);
        transform.rotation = Quaternion.Euler(new Vector3(0, turnVector, 0));

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.gameObject.transform.position.x > leftSide)
            {
                transform.Translate(horizontalSpeed * Time.deltaTime * Vector3.left);
                transform.rotation = Quaternion.Euler(new Vector3(0, turnVector, 0));
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (this.gameObject.transform.position.x < rightSide)
            {
                transform.Translate(-1 * horizontalSpeed * Time.deltaTime * Vector3.left);
                transform.rotation = Quaternion.Euler(new Vector3(0, turnVector, 0));
            }
        }

        GetComponent<Animator>().SetFloat("Turning", turnVector);
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
            Death();
        }
    }

    public void Death()
    {
        alive = false;
        //Invoke(nameof(Restart), 2);
        canvas.GetComponent<UiScript>().DeathCanvas();
        GetComponent<Animator>().SetTrigger("Death");
    }

    public void SmokePlay() { smoke.Play(); }
    public void SmokeStop() { smoke.Stop(); }



    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Finish"))
        {
            canvas.GetComponent<Finish>().FinishSeq();
        }
    }
}
