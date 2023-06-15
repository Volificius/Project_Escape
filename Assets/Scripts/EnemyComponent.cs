using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject[] itemsToDrop;

    private Rigidbody2D rb;
    private PlayerAwareness playerAwareness;
    private Vector2 targetDirection;

    [SerializeField] public GameObject gun;

    public int enemyHealth;

    private float time = 0.0f;
    public float interpolationPeriod = 1.0f;

    public float restartPeriod = 4.0f;
    
    public Transform enemy;

    public Animator animator;
    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
        player = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
        ShootDelay();
        Restart();
    }

    private void UpdateTargetDirection() 
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;

        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget() 
    {
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity() 
    {
        if (targetDirection == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
        else 
        {
            rb.velocity = transform.up * speed;
        }
        animator.SetFloat("Walk Speed", rb.velocity.magnitude * 2.0f);
    }

    // Enemy health
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            
            TakeDamage(20);
            
            playerAwareness.AwareOfPlayer = true;
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth = System.Math.Max(enemyHealth - damage, 0);

        if (enemyHealth == 0)
        {
            for (int i = 0; i < itemsToDrop.Length; i++)
            {
                Instantiate(itemsToDrop[i], transform.position + new Vector3(Random.Range(-1,1), Random.Range(-1, 1), 0), Quaternion.identity);
            }
            FindObjectOfType<AudioManager>().Play("enemyDeath");

            Destroy(gameObject);
        }
        FindObjectOfType<AudioManager>().Play("humanHit");
    }

    public void ShootDelay() 
    {
        if (playerAwareness.AwareOfPlayer)
        {
            time += Time.deltaTime;

            if (time >= interpolationPeriod)
            {
                time = time - interpolationPeriod;

                gun.GetComponent<GunComponent>().Shoot();
            }
        }
    }

    public void Restart()
    {
        if (player == null)
        {
            time += Time.deltaTime;
        
            if (time >= restartPeriod)
            {
                time = time - restartPeriod;
            
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

}
