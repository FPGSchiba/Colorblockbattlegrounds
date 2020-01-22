using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float width;

    void Update()
    {
        GameObject su = GameObject.Find("trigger");
        TriggerSaug triggerSaug = su.GetComponent<TriggerSaug>();
        width = triggerSaug.cube;
        width = width * 2;

        GameObject bar = GameObject.Find("Progress");
        var theBarRectTransform = bar.transform as RectTransform;
        theBarRectTransform.sizeDelta = new Vector2(width, theBarRectTransform.sizeDelta.y);
    }

}
