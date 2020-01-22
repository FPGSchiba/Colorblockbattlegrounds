using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void OpenSandbox()
    {
        SceneManager.LoadScene("MinecraftEnd");
    }

    public void OpenRaumschiff()
    {
        SceneManager.LoadScene("Labyrinth");
    }
}
