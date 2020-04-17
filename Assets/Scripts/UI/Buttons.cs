using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameManager manager;
    public PauseMenu menu;
    public GameObject options;

    public void OpenSandbox()
    {
        SceneManager.LoadScene(8);
    }

    public void OpenRaumschiff()
    {
        SceneManager.LoadScene(9);
    }

    public void OpenMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RespawnOnSandbox()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.OnPlayerRespawn();
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(6);
    }

    public void OpenOptoinsSandbox()
    {
        options.SetActive(true);
    }

    public void ResumeOnSandbox()
    {
        menu.Resume();
    }

    public void CloseOptionsOnSandbox()
    {
        options.SetActive(false);
    }
}
