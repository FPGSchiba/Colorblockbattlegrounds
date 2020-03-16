using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{

    [Header("Variablen")]
    [SerializeField]
    [GreyOut]
    float count;
    [SerializeField]
    float walkRadius;

    [Header("Referenzen")]
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    EnemyAI Ai;

    void Start()
    {
        agent.SetDestination(randomVector3());
    }

    void Update()
    {
        
        if(count > 5 && !Ai.SeesPlayer)
        {
            count = 0;
            agent.SetDestination(randomVector3());
        }
        else
        {
            count = count + 1 * Time.deltaTime;
        }

    }

    public Vector3 randomVector3()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;

        return finalPosition;
    }
}
