using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    [Header("Damage to Player")]
    public int damage;

    private void Awake()
    {
        // setting the variable without manual work
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerObj").transform;
    }

    void Update()
    {
        // chase player
        agent.SetDestination(player.position);
    }
    void OnCollisionEnter(Collision collision)  // colliding with a player object
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);    //  destroys the enemy
            // damage is set within the inspector
            PlayerManager.Instance.damagePlayer(damage);
        }
    }
}
