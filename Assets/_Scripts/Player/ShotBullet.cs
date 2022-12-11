using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    float bulletSpeed = 1000f;

    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
    }
    public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.GetComponent<Collector>().boxes.Count <= 1) { return; }
            else
            {
                GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
                Rigidbody tempBulletRigidbody = tempBullet.GetComponent<Rigidbody>();
                tempBulletRigidbody.AddForce(Vector3.forward * bulletSpeed);
                Destroy(tempBullet, .7f);
            }
        }
    }
}
