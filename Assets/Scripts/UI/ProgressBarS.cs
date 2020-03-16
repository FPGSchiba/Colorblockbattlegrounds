using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarS : MonoBehaviour
{
    [SerializeField]
    [GreyOut]
    float width;
    [SerializeField]
    Slider slider;
    GameObject su;

    private void Start()
    {
        slider = GameObject.Find("ShootAnz").GetComponent<Slider>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {

            su = GameObject.Find("Piu");
            Schusslader triggerSaug = su.GetComponent<Schusslader>();
            width = triggerSaug.speed;
            width = width * 100 / 4000;

            slider.value = width;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            slider.value = 0;
        }
    }
}
