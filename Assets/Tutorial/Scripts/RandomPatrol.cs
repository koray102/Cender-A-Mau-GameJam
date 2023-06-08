using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsGround;

    //Patrolling
    public Vector3 walkpoint;
    bool WalkPointSet;
    public float WalkPointRange;
    public float speed = 3.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        agent.speed = speed;
        Patrolling();
    }

    private void Patrolling()
    {
        if (!WalkPointSet) SearchWalkPoint();
        if (WalkPointSet)
            agent.SetDestination(walkpoint);


        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            WalkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-WalkPointRange, WalkPointRange);
        float randomX = Random.Range(-WalkPointRange, WalkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
            WalkPointSet = true;
    }
}
