using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level;
    public float exp;

    public LevelData (Level lev)
    {
        level = lev.level;
        exp = lev.exp;
    }
}
