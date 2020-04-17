using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarShipLoading : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(asyncLoad());
    }

    IEnumerator asyncLoad()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation LoadGame = SceneManager.LoadSceneAsync(3);

        yield return new WaitForEndOfFrame();
    }
}
