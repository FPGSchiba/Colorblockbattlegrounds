using UnityEngine;
using System.Threading;

public class Ruckstoss : MonoBehaviour
{
    public GameObject ruck;

    // Update is called once per frame
    void Update()
    {

        GameObject fo = GameObject.Find("Piu");
        Schusslader schusslader = fo.GetComponent<Schusslader>();
        float force = schusslader.force;

        if (Input.GetButtonUp("Fire1"))
        {
            GameObject ru = Instantiate(ruck);
            ru.transform.position = GameObject.Find("Ruck").transform.position;
            Rigidbody rb = ru.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * force);
        }

    }
}
