using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashy : MonoBehaviour
{

    public bool Light = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {

            Light = !Light;

            GameObject.Find("Flash").GetComponent<Light>().enabled = Light;

        }

    }
}
