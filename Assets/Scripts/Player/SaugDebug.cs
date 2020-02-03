using UnityEngine;

public class SaugDebug : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            GameObject.Find("trigger").GetComponent<SphereCollider>().enabled = false;
        }
    }
}
