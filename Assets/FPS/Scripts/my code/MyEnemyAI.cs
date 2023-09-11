using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MyEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float timeAttacks;
    private bool isAttacked;

    public float sightRange, attackRange;
    private bool playerInSight, playerInAttack;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent.GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttack = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSight && !playerInAttack)
        {
            Patrol();
        }

        if (playerInSight && !playerInAttack)
        {
            ChasePlayer();
        }

        if (playerInAttack && playerInSight)
        {
            AttackPlayer();
        }
    }

    void Patrol()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distance = transform.position - walkPoint;

        if (distance.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        float RanZ = Random.Range(-walkPointRange, walkPointRange);
        float RanX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RanX, transform.position.y, transform.position.z + RanZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!isAttacked)
        {
            //add attack code here 
            
            isAttacked = true;
            Invoke(nameof(ResetAttack), timeAttacks);
        }
    }

    void ResetAttack()
    {
        isAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
