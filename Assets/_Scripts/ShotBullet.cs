using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    float bulletSpeed = 1000f;

   public void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tempBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            Rigidbody tempBulletRigidbody = tempBullet.GetComponent<Rigidbody>();
            tempBulletRigidbody.AddForce(Vector3.forward * bulletSpeed);
            Destroy(tempBullet, .7f);
        }
    }
}
