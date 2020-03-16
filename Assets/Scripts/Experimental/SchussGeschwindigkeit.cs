using Project.Utility;
using UnityEngine;

public class SchussGeschwindigkeit : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField]
    LayerMask Player;

    [Header("Variablen")]
    [SerializeField]
    [GreyOut]
    Vector3 Oldposition;
    [SerializeField]
    [GreyOut]
    float Speed;
    [SerializeField]
    [GreyOut]
    float count;
    [SerializeField]
    [GreyOut]
    public GameObject shooter;

    private void Start()
    {
        Collider[] MaybeShooter = Physics.OverlapSphere(transform.position, 10, Player);

        foreach(Collider i in MaybeShooter)
        {
            if(i.gameObject.layer == 10)
            {
                shooter = i.gameObject;
            }
        }
    }

    private void FixedUpdate()
    {
        if(count >= 1)
        {
            Oldposition = transform.position;
        }
        else
        {
            count = count + 1 * Time.deltaTime;
        }
    }

    public float SchussSpeed()
    {
        Speed = (transform.position - Oldposition).magnitude.TwoDecimals();

        return Speed;
    }
}