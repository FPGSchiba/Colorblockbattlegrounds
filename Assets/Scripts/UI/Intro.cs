using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [GreyOut]
    public float count;

    private void Update()
    {
        if(count >= 10)
        {
            SceneManager.LoadScene(1);
        }

        count += 1 * Time.deltaTime;
    }
}
