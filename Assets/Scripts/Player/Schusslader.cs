using UnityEngine;

public class Schusslader : MonoBehaviour
{
    public float speed = 1000f;
    public float SpeedMax = 10000f;
    float speedstart;
    public float PlusProFrame = 100f;
    public float Knockbäck = 1000f;
    public float force;
    [SerializeField]
    PlayerMove move;
    [GreyOut]
    public bool isShooted;
    [SerializeField]
    public GameObject schuss;

    private void Start()
    {
        GameObject schuss = new GameObject();
        speedstart = speed;
    }

    void Update()
    {
        GameObject su = GameObject.Find("trigger");
        TriggerSaug triggerSaug = su.GetComponent<TriggerSaug>();

        if (triggerSaug.cube > 0)
        {

            if (Input.GetButton("Fire1") && !Input.GetMouseButton(1) && !PauseMenu.isPaused && !GameManager.isDead)
            {
                if (speed < SpeedMax)
                {
                    GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
                    speed = speed + PlusProFrame;
                }

            }

            if (Input.GetButtonUp("Fire1") && !Input.GetMouseButton(1) && !PauseMenu.isPaused && !GameManager.isDead)
            {
                GameObject piu = Instantiate(schuss, GameObject.Find("Piu").transform.position, GameObject.Find("Piu").GetComponent<Transform>().localRotation);
                Rigidbody rb = piu.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * speed);
                force = speed / 10;
                speed = speedstart;
                triggerSaug.cube--;
                isShooted = true;
                SchussGeschwindigkeit schussGeschwindigkeit = GameObject.Find("SchussRot(Clone)").GetComponent<SchussGeschwindigkeit>();
            }

        }

        isShooted = false;

    }

}
