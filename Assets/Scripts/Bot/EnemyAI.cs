using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{

    [Header("Variablen")]
    public float lookRadius = 50f;
    public float shootRadius = 200f;
    [GreyOut]
    public bool SeesPlayer;
    [GreyOut]
    [SerializeField]
    float count;
    [SerializeField]
    [Range(0f, 5f)]
    float ShootCooldown;
    [Header("Referenzen")]
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    [SerializeField]
    BotShoot shoot;

    void Start()
    {
        SeesPlayer = false;
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= shootRadius && SeesPlayer)
        {
            agent.speed = 0;
            transform.LookAt(target);

            if(count >= ShootCooldown)
            {
                count = 0;
                shoot.OnShoot();
            }
            else
            {
                count = count + 1 * Time.deltaTime;
            }
        }
        else if (SeesPlayer)
        {
            agent.speed = 50;
        }
        else if (distance <= lookRadius)
        {
            agent.speed = 50;
            agent.SetDestination(target.position);
        }
    }

    public void RepawnBot()
    {
        SeesPlayer = false;
        agent = this.GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }
}
