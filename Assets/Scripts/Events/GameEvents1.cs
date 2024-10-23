using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents1 : MonoBehaviour
{
    public static GameEvents1 instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Data Persistance Manager in this Scene.");
        }
        instance = this;
    }

    public event Action onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter()
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter();
        }
    }
}
    