using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Slider slider;
    public Text Text;
    public int level = 1;
    public float exp = 0;
    float NeededEXP = 100;

    public void OnApplicationQuit()
    {
        LevelSave.SaveLevel(this);
    }

    public void OnLoadLevel()
    {
        LevelData data = LevelSave.LoadLevel();

        level = data.level;
        exp = data.exp;
    }

    public void ExpErhöhen(float amount)
    {
        exp += amount;

        if(exp >= NeededEXP)
        {
            LevelUp();
            exp = 0;
        }
        ChangeLevel();
    }

    public void LevelUp()
    {
        level++;
        NeededEXP = NeededEXP * 1.5f;
    }

    private void Start()
    {
        OnLoadLevel();
        ChangeLevel();
    }

    public void ChangeLevel()
    {
        slider.value = exp * 100 / NeededEXP;
        Text.text = "Level: " + level;
    }
}
