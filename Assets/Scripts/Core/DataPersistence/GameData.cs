using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class GameData
{
    public float maxHP;
    public float currentHP;

    public GameData()
    {
        maxHP = 100;
        currentHP = 100;
    }
}
