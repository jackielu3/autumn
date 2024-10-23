using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private void GameOver()
    {
        Debug.Log("You Died UWU");
    }
}
