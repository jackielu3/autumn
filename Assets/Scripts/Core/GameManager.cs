using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("References")]
    public DynamicInventory playerInventory;

    private void Awake()
    {
        playerInventory.Initialize();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.PageUp))
        {

        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
            
        }
    }

    private void GameOver()
    {
        Debug.Log("You Died UWU");
    }
}
