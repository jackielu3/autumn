using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public int maxHP;
    public int currentHP;

    public GameData()
    {
        maxHP = 100;
        currentHP = 100;
    }
}
