using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Saug : MonoBehaviour
{


    // Start is called before the first frame update

    public float radius = 5.0F;
    public float power = 10.0F;

    private void Start()
    {
        GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
    }

    void Update()
    {

        if (Input.GetMouseButton(1) && !Input.GetButton("Fire1"))
        {
            GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = true;
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 1.0F);
            }
        }
        else
        {
            GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
        }

    }

}