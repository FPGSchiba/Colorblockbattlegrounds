using System.Collections;
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
    NavMeshAgent agent;
    [SerializeField]
    EnemyAI AI;
    [SerializeField]
    EnemyLibrary Library;
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    GameObject NonFocus;

    void Update()
    {
        Collider[] cubes = Physics.OverlapSphere(transform.position, DogRadius, Cube);

        foreach(Collider col in cubes)
        {
            Library.AktuellDoging(agent, col, DogRadius);
            StartCoroutine(DogingTheCube());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DogRadius);
    }

    IEnumerator DogingTheCube()
    {
        yield return new WaitForSeconds(1);
        agent.speed = AI.SpeedReset;
        agent.SetDestination(manager.GetTarget(Enemy, NonFocus).position);
    }
}
