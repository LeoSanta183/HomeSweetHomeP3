using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CarrotAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float range;

    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttack, isStunned;

    AudioManager audioManager;
    private bool hasPlayedGrunt = false;

    PlayerHealth pHealthScript;

    Animator animate;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        pHealthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        animate = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
    playerInAttack = Physics.CheckSphere(transform.position, attackRange, playerLayer);

    if (!playerInSight && !playerInAttack && !isStunned) 
        {
        Patrol();
        hasPlayedGrunt = false; // Reset the flag if the player is not in sight
        }
    if (playerInSight && !playerInAttack && !isStunned && !hasPlayedGrunt) 
        {
        Chase();
        audioManager.PlaySFX(audioManager.CarrotGrunt);
        hasPlayedGrunt = true; // Set the flag after playing the sound
        }
    if (playerInSight && playerInAttack) Attack();
    }

        
    

    void Attack()
    {
        animate.SetTrigger("CarrotAttack");
    }
    void Chase()
    {
        agent.SetDestination(player.transform.position);
        
    }

    void Patrol()
    {
        if (!walkPointSet) SearchForDest();
        if (walkPointSet) agent.SetDestination(destPoint);
        if (Vector3.Distance(transform.position, destPoint) < 10) walkPointSet = false;

    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        destPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
            walkPointSet = true;
        }
    }
}
