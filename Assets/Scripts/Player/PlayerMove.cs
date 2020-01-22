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

    private void Start()
    {

    }

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

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Knockback(float amount)
    {
        GetComponent<Rigidbody>().AddForce(Vector3.back * amount);
    }
}
