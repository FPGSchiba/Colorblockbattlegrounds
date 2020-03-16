using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    [GreyOut]
    float width;

    Slider slider;

    private void Start()
    {
        slider = GameObject.Find("CubesPlayer").GetComponent<Slider>();
    }

    void Update()
    {
        GameObject su = GameObject.Find("trigger");
        TriggerSaug triggerSaug = su.GetComponent<TriggerSaug>();
        width = triggerSaug.cube;
        width = width * 2;

        slider.value = width;
    }
}
