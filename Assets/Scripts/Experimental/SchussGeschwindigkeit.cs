using Project.Utility;
using UnityEngine;

public class SchussGeschwindigkeit : MonoBehaviour
{

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


    void Start()
    {

        Oldposition = transform.position;

    }

    void FixedUpdate()
    {
        
        Speed = (transform.position - Oldposition).magnitude.TwoDecimals();

        Oldposition = transform.position;

        if(Speed == 0)
        {
            count = count + 1 * Time.deltaTime;

            if(count == 5)
            {
                Debug.Log("Destroy please");
            }
        }

    }

}
