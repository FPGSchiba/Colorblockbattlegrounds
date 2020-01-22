using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    public void AnimEnd(GameObject anim)
    {
        anim = GameObject.Find("SchussRot");
        anim.GetComponent<Animator>().enabled = false;
    }
}
