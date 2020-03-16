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
    [SerializeField]
    EnemyLibrary Library;

    void Start()
    {
        agent.SetDestination(Library.randomVector3(walkRadius));
    }

    void Update()
    {
        
        if(count > 5 && !Ai.SeesPlayer)
        {
            count = 0;
            agent.SetDestination(Library.randomVector3(walkRadius));
        }
        else
        {
            count = count + 1 * Time.deltaTime;
        }

    }
}
