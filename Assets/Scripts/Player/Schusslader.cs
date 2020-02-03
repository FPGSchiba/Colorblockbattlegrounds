using UnityEngine;

public class Schusslader : MonoBehaviour
{
    public float speed = 1000f;
    public float SpeedMax = 10000f;
    public float PlusProFrame = 100f;
    public float Knockbäck = 1000f;
    public float force;
    [SerializeField]
    GameObject schuss = new GameObject();
    [SerializeField]
    PlayerMove move;
    [GreyOut]
    public bool isShooted;
    GameObject piu;

    void Update()
    {
        GameObject su = GameObject.Find("trigger");
        TriggerSaug triggerSaug = su.GetComponent<TriggerSaug>();
        float cu = triggerSaug.cube;

        if (cu > 0)
        {

            if (Input.GetButton("Fire1") && !Input.GetMouseButton(1))
            {
                if (speed < SpeedMax)
                {
                    GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
                    speed = speed + PlusProFrame;
                }

            }

            if (Input.GetButtonUp("Fire1") && !Input.GetMouseButton(1))
            {
                piu = Instantiate(schuss, GameObject.Find("Piu").transform.position, GameObject.Find("Piu").GetComponent<Transform>().localRotation);
                Rigidbody rb = piu.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * speed);
                force = speed / 10;
                speed = 1000f;
                triggerSaug.cube--;
                isShooted = true;

            }

        }

        isShooted = false;

    }

}
