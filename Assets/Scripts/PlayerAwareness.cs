using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool AwareOfPlayer { get; set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;
    public GameObject playerGameObject;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (player == null)
        {
            AwareOfPlayer = false;
            return;
        }

        Vector2 enemyToPlayerVector = player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance || AwareOfPlayer)
        {
            AwareOfPlayer = true;
        }
        else 
        {
            AwareOfPlayer = false;
        }

        
    }
}
