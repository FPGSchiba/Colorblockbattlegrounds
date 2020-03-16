using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Doging : MonoBehaviour
{
    [Header("Variablen")]
    [SerializeField]
    float DogRadius;

    [Header("Referenzen")]
    [SerializeField]
    LayerMask Cube;
    [SerializeField]
    Transform target;
    [SerializeField]
    NavMeshAgent agent;

    void Update()
    {
        Collider[] cubes = Physics.OverlapSphere(transform.position, DogRadius, Cube);

        foreach(Collider col in cubes)
        {
            if(col.gameObject.tag == "cube")
            {
                NavMeshHit hit;
                NavMesh.SamplePosition(transform.right, out hit, DogRadius, 1);
                Vector3 finalPosition = hit.position;
                agent.speed = 1000;
                agent.SetDestination(finalPosition);
                StartCoroutine(DogingTheCube());
            }
        }
    }

    IEnumerator DogingTheCube()
    {
        yield return new WaitForSeconds(1);
        agent.speed = 50;
        agent.SetDestination(target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DogRadius);
    }
}
