using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [Range(0, 10f)] public float walkSpeed;
    [Range(0, 20f)] public float sprintSpeed;

    [Range(0, 100)] public int maxHealth;
    public int health;
    public HealthBar healthBar;
    // public Collision coll;
    [SerializeField] public GameObject gun;


    public bool sprint;

    public Rigidbody2D rb;
    public Camera cam;

    public Vector2 movement;
    public Vector2 mousePos;

    public GameObject healthBox;

    public Animator animator;
    
    private void Awake()
    {
        health = maxHealth;

        healthBar.SetHealth(health, maxHealth, true);
    }

    void Update()
    {
        //Input

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;


        if (Input.GetButton("Shift"))
        {
            sprint = true;
        }
        else
        {
           sprint = false;
        }

        // Converting mouse position to a worldpoint
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetButtonDown("Fire1"))
        {
            gun.GetComponent<GunComponent>().Shoot();
        }

    }
        

    // Health

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            TakeDamage(10);
            FindObjectOfType<AudioManager>().Play("humanHit");
            CinemachineShake.Instance.ShakeCamera(0.5f, 0.2f);
        }

    }

    public void TakeDamage(int damage) 
    {
        health = System.Math.Max(health - damage, 0);

        healthBar.SetHealth(health, maxHealth);

        if (health == 0)
        {
            FindObjectOfType<AudioManager>().Play("playerDeath");

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //Movement
        var speed = movement * walkSpeed;
        
        if (sprint)
        {
            speed = movement * sprintSpeed;
        }

        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime);

        animator.SetFloat("Walk Speed", speed.magnitude);
        

        // look position vector
        Vector2 lookDir = mousePos - rb.position;
        
        // player rotation to look at the mouse 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

    }
}
