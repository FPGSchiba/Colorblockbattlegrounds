using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField]
    GameObject Schuss;
    [SerializeField]
    GameManager manager;
    [SerializeField]
    GameObject Enemy;
    [SerializeField]
    GameObject NonFocus;

    public void OnShoot()
    {
        transform.LookAt(manager.GetTarget(Enemy, NonFocus));

        GameObject pau = Instantiate(Schuss, transform.position, transform.localRotation);
        Rigidbody rb = pau.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 4000);
        //Cube vom Inventar abziehen
    }
}
