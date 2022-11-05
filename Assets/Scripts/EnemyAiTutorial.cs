// Reference: 
//   - https://www.youtube.com/watch?v=UjkSFoLxesw&t=6s&ab_channel=Dave%2FGameDevelopment
//   - https://github.com/Unity-Technologies/NavMeshComponents

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAiTutorial : MonoBehaviour
{
    // Defining variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    private List<GameObject> projectilesList = new List<GameObject>();

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public float lastDidSomething, pauseTime = 1f;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Pauses for a second after some action  
        if (Time.time < lastDidSomething + pauseTime){
            return;
        }
 
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange){
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange){
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (projectilesList.Count > 0){
            foreach (var projec in projectilesList){
                Destroy(projec);
            }
        }

        if (!walkPointSet){
            SearchWalkPoint();
        }
        if (walkPointSet){
            agent.SetDestination(walkPoint);
        }
            
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
            
        lastDidSomething = Time.time;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }

        lastDidSomething = Time.time;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        lastDidSomething = Time.time;
    }

    private void AttackPlayer()
    {
        //Make sure enemy look at player to attack
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked){
            if (projectilesList.Count > 0){
                foreach (var projec in projectilesList){
                    Destroy(projec);
                }
            }
            //Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            projectilesList.Add(rb.gameObject);
            //End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
        lastDidSomething = Time.time;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        lastDidSomething = Time.time;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0){
            Invoke(nameof(DestroyEnemy), .5f);
        }
        lastDidSomething = Time.time;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        
    }
}
