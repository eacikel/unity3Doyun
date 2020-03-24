using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    float moveSpeed = 1200f;
    float timeFromLastShoot;

    public void Shoot(float shootFreq){

        if ((timeFromLastShoot+=Time.deltaTime)>= 1/shootFreq)  {

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
            timeFromLastShoot = 0;
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
    }
}
