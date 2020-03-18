using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    GameManager manager;

    public void OpenSandbox()
    {
        SceneManager.LoadScene("MinecraftEnd");
    }

    public void OpenRaumschiff()
    {
        SceneManager.LoadScene("Labyrinth");
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
}
