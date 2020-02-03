using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jump = 3f;
    [GreyOut]
    Vector3 velocity;
    [GreyOut]
    bool grounded;
    [SerializeField]
    Schusslader schusslader;
    [Header("Variablen")]
    [SerializeField]
    float strength;
    [SerializeField]
    float Mittelwert;
    Vector3 moveDirection;
    [SerializeField]
    [GreyOut]
    public float Ruckcount = 0.2f;
    [SerializeField]
    Transform cam;
    bool isCount;

    void Update()
    {

        grounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y += Mathf.Sqrt(jump * -2f * gravity);
        }

        if (Input.GetButtonUp("Fire1") && Ruckcount > 0.2 && !Input.GetMouseButton(1))
        {
            Ruckcount = 0;
            moveDirection = cam.transform.TransformDirection(Vector3.forward) * -schusslader.force / 500;
        }

        if(Ruckcount < 0.2)
        {
            isCount = true;
            controller.Move(moveDirection);
            Ruckcount = Ruckcount + 1 * Time.deltaTime;
            if (schusslader.isShooted)
            {
                GameObject.Find("Piu").GetComponent<Schusslader>().enabled = false;
            }
        }
        else
        {
            isCount = false;
            GameObject.Find("Piu").GetComponent<Schusslader>().enabled = true;
        }

        if(velocity.z != 0 && isCount == false)
        {
            velocity.z = 0;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        velocity = controller.velocity;
    }
}
