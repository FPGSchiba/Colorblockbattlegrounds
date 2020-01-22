using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarS : MonoBehaviour
{
    public float width;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {

            GameObject su = GameObject.Find("Piu");
            Schusslader triggerSaug = su.GetComponent<Schusslader>();
            width = triggerSaug.speed;
            width = width * 100 / 4000;

            GameObject bar = GameObject.Find("ProgressS");
            var theBarRectTransform = bar.transform as RectTransform;
            theBarRectTransform.sizeDelta = new Vector2(width, theBarRectTransform.sizeDelta.y);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            width = 0;

            GameObject bar = GameObject.Find("ProgressS");
            var theBarRectTransform = bar.transform as RectTransform;
            theBarRectTransform.sizeDelta = new Vector2(width, theBarRectTransform.sizeDelta.y);
        }

    }

}
