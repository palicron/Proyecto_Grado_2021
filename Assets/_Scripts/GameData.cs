using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData 
{
    public int level;
    public bool[] progress;

    public GameData(int Curlevel, bool[] progreso)
    {
        level = Curlevel;
        progress = progreso;
    }
}
