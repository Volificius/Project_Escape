using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player") )
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        

        Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        Destroy(gameObject);

        //var rb = collision.gameObject.GetComponent<Rigidbody2D>();
        //rb.AddForce(collision.)

    }
}
