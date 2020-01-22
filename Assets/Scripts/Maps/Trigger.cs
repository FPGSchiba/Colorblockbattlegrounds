using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
    public float cu;

    private void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "cube")
        {
            Destroy(col.gameObject);
            cu++;
            Debug.Log(cu);
        }
    }
}
