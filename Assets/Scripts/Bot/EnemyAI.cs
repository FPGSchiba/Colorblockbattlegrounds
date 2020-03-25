using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{

    [Header("Variablen")]
    public float lookRadius = 50f;
    public float shootRadius = 200f;
    [GreyOut]
    public bool SeesPlayer;
    [GreyOut]
    [SerializeField]
    float count = 0f;
    [SerializeField]
    [Range(0f, 5f)]
    float ShootCooldown = 2f;
    [SerializeField]
    public float SpeedReset= 25f;
    [SerializeField]
    [GreyOut]
    public float streak;
    public List<string> Keys;

    [Header("Referenzen")]
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    BotShoot shoot;
    [SerializeField]
    EnemyLibrary Library;
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    GameObject NonFocus;
    [SerializeField]
    BotHealth health;

    void Start()
    {
        SeesPlayer = false;
    }

    void Update()
    {
        Shoot();
        SetTeam();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
    }

    public void Shoot()
    {
        if (Library.distance(manager.GetTarget(Enemy, NonFocus), transform) <= shootRadius && SeesPlayer)
        {
            agent.speed = 0;
            transform.LookAt(manager.GetTarget(Enemy, NonFocus));

            if (count >= ShootCooldown)
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
            agent.speed = SpeedReset;
        }
        else if (Library.distance(manager.GetTarget(Enemy, NonFocus), transform) <= lookRadius)
        {
            agent.speed = SpeedReset;
            agent.SetDestination(manager.GetTarget(Enemy, NonFocus).position);
        }
    }

    void SetTeam()
    {
        string thisTeam = manager.GetTeam(this.gameObject);

        if(thisTeam == "keins")
        {
            Debug.LogWarning("This Object doesnt have a Team. Warning Code: 420");
            this.gameObject.SetActive(false);
            Debug.LogWarning("Warning Code: 420 fixed");
        }
        else if(thisTeam == "blue")
        {
            health.SetColor("blue");
        }
        else if(thisTeam == "red")
        {
            health.SetColor("red");
        }
    }
}
