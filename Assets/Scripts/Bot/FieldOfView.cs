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
    public EnemyLibrary Library;

    private void Start()
    {
        StartCoroutine("FindtargetsWithDelay", .2f);
    }

    IEnumerator FindtargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Library.FindVisibleTargets(viewRadius, targetMask, viewAngle, obstacleMask, Enemy, agent);
        }
    }

    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {

        return Library.DirFromAnglee(angleDegrees, angleIsGlobal);
    }
}
