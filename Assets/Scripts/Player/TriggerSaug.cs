using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSaug : MonoBehaviour
{

    public float cube = 0F;

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "cube")
        {

            if(cube < 50)
            {

                Destroy(col.gameObject);
                cube++;

            }

        }

    }

    private void Update()
    {
        if(cube < 50)
        {

            GameObject.Find("Saug").GetComponent<Saug>().enabled = true;

        }
        else
        {

            GameObject.Find("Saug").GetComponent<Saug>().enabled = false;

        }
    }

}
