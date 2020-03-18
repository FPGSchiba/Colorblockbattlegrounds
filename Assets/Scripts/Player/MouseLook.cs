using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public Transform playerBody;
    [GreyOut]
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * SharedSavedStuff.Sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SharedSavedStuff.Sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}

public class SharedSavedStuff
{
    public static float Sensetivity;
}

