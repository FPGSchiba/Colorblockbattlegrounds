using UnityEditor;
using UnityEngine;
using System.Threading;

public class Schusslader : MonoBehaviour
{
    public float speed = 1000f;
    public float SpeedMax = 10000f;
    public float PlusProFrame = 100f;
    public float force;
    public GameObject schuss;

    void Update()
    {
        GameObject su = GameObject.Find("trigger");
        TriggerSaug triggerSaug = su.GetComponent<TriggerSaug>();
        float cu = triggerSaug.cube;

        if(cu > 0)
        {

            if (Input.GetButton("Fire1"))
            {
                if (speed < SpeedMax)
                {
                    GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
                    speed = speed + PlusProFrame;
                }

            }

            if (Input.GetButtonUp("Fire1"))
            {
                GameObject piu = Instantiate(schuss);
                piu.transform.position = GameObject.Find("Piu").transform.position;
                Rigidbody rb = piu.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * speed); ;
                force = speed / 10;
                speed = 1000f;
                triggerSaug.cube--;

            }

        }

    }

}
