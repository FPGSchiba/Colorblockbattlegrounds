using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLibrary : MonoBehaviour
{
    public float distance(Transform target, Transform transform)
    {
        float dist = Vector3.Distance(target.position, transform.position);

        return dist;
    }

    public void AktuellDoging(NavMeshAgent agent, Collider col, float DogRadius)
    {
        if (col.gameObject.tag == "cube")
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(transform.right, out hit, DogRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.speed = 1000;
            agent.SetDestination(finalPosition);
        }
    }

    public Vector3 DirFromAnglee(float angleDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }

    public void FindVisibleTargets(float viewRadius, LayerMask targetMask,float viewAngle, LayerMask obstacleMask, EnemyAI Enemy, NavMeshAgent agent)
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    bool nearEnemy = Vector3.Distance(transform.position, target.position) <= Enemy.shootRadius;

                    if (!nearEnemy)
                    {
                        agent.SetDestination(target.position);
                    }

                    Enemy.SeesPlayer = true;
                }
                else
                {
                    Enemy.SeesPlayer = false;
                }
            }
            else
            {
                Enemy.SeesPlayer = false;
            }
        }

        if (targetsInViewRadius == null)
        {
            Enemy.SeesPlayer = false;
        }
    }

    public void OnSchussEnter(float Speed, bool isEnemy, BotHealth health)
    {
        if (isEnemy)
        {
            float damage = 0;

            damage = (Speed / 19.62f).TwoDecimals();

            if (damage < 0)
            {
                damage = damage * -1;
            }

            health.SetHealth(damage);
        }
    }

    public float CalculateHealthforBar(float health, float MaxHealth)
    {
        return health / MaxHealth;
    }


    public void Dead(GameManager manager, GameObject Enemy)
    {
        manager.OnDeathBot(Enemy, 10);
    }

    public Vector3 randomVector3(float walkRadius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;

        return finalPosition;
    }
}
