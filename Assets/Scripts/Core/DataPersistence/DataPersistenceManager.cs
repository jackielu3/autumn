using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }

    private string filePath;
    private GameData gameData;
    private List<ISaveable> saveableObjects;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        filePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }


    private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Refresh list of saveable objects in the new scene
        FindSaveableObjects();

        LoadGame();
    }

    private void Start()
    {
        FindSaveableObjects();
    }

    // TODO REMOVE!!!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            Debug.Log("uwu");
            SaveGame();
        }
    }



    // TODO TEMP!!
    public void OnApplicationQuit()
    {
        SaveGame();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log(filePath);
            // No save file yet, use default values
            Debug.Log("No save file found. Creating new GameData with defaults.");
            gameData = new GameData();
            return;
        }
        try
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(json);

            foreach (var saveable in saveableObjects)
            {
                saveable.LoadData(gameData);
            }

            Debug.Log($"Game loaded form {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Error reading save file: {e}");
            gameData = new GameData();
        }
    }

    public void SaveGame()
    {
        gameData ??= new GameData();

        foreach (var saveable in saveableObjects)
        {
            saveable.SaveData(ref gameData);
        }

        try
        {
            string json = JsonUtility.ToJson(gameData, prettyPrint: true);
            File.WriteAllText(filePath, json);
            Debug.Log($"Game saved to {filePath}");
        }
        catch (IOException e)
        {
            Debug.LogError($"Error writing save file: {e}");
        }
    }

    // Find every MonoBehaviour in the scene that implements ISaveable (even if disabled).
    public void FindSaveableObjects()
    {
        saveableObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<ISaveable>()
            .ToList();
    }
}