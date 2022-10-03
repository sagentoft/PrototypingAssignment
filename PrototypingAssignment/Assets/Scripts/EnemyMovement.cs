using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask terrain, isPlayer;

    //RandomlyMoving
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float attackSpeed;
    public bool alreadyAttacked;
    //AttackingVariablesHere
    
    //

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Checking Attack range/sightRange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasingPlayer();
        if (playerInSightRange && playerInAttackRange) Attacking();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distnaceToWalkPoint = transform.position - walkPoint;

        // Walkpont Reached
        if (distnaceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, terrain))
            walkPointSet = true;
    }
    private void Attacking()
    {
        //Enemies dont get to move and attack at the same time
        agent.SetDestination(transform.position);


    }
    private void ChasingPlayer()
    {
        agent.SetDestination(player.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            //PutAttacking stuff Here!!

            //---------------
            alreadyAttacked = true;
            Invoke(nameof(resetAttack), attackSpeed);
        }

    }

    private void resetAttack()
    {
        alreadyAttacked = false;
    }
}
