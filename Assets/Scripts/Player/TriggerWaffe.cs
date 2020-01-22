using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWaffe : MonoBehaviour
{
    public Transform Saug;
    public Transform Trigger;

    void Update()
    {

        Trigger.position = Saug.position;

    }


}

