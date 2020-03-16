using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField]
    GameManager manager;

    private void OnCollisionEnter(Collision col)
    {

        if(col.gameObject.tag == "cube")
        {
            Destroy(col.gameObject);
            manager.BlockDeleted(1);
        }
    }
}
