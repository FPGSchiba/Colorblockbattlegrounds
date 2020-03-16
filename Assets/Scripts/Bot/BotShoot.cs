using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{

    [Header("Variablen")]
    [SerializeField]
    [GreyOut]
    float dist;
    [SerializeField]
    [GreyOut]
    float time;
    [SerializeField]
    [GreyOut]
    float Höhe;
    [SerializeField]
    [GreyOut]
    float Rotation;

    [Header("Referenzen")]
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject Schuss;

    public void OnShoot()
    {
        transform.LookAt(target);

        GameObject pau = Instantiate(Schuss, transform.position, transform.localRotation);
        Rigidbody rb = pau.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 4000);
        //Cube vom Inventar abziehen
    }
}
