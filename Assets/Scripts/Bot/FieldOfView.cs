using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public EnemyAI Enemy;

    private void Start()
    {
        StartCoroutine("FindtargetsWithDelay", .2f);
    }

    IEnumerator FindtargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
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

        if(targetsInViewRadius == null)
        {
            Enemy.SeesPlayer = false;
        }
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}
