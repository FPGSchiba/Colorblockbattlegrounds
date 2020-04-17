using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoonLoading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(asyncLoad());
    }

    IEnumerator asyncLoad()
    {
        yield return new WaitForSeconds(3);
        //Die Map noch Anpassen also den Int im LoadScene
        AsyncOperation LoadGame = SceneManager.LoadSceneAsync(2);

        yield return new WaitForEndOfFrame();
    }
}
