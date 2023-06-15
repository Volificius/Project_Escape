using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject shootEffect;

    [Range (0,40f)] public float bulletForce;

   

    public void Shoot() 
    {
        FindObjectOfType<AudioManager>().Play("Gun1");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect, firePoint.position, firePoint.rotation, transform);
        Destroy(effect, 0.1f);

    }
}
