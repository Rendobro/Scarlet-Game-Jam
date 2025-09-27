using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // needs to be implemented


    private bool endlessEnabled = false;
    private int roundNumber = 0;
    private int enemiesRemaining = 0;
    [SerializeField] private Dictionary<GameObject, List<int>> enemiesToSpawn;
    private int enemiesKilled = 0;
    [SerializeField] private int roundDuration = 60; // seconds
    [SerializeField] private float spawnInterval = 2f; // seconds
    [SerializeField] private List<GameObject> enemyPrefabs;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    void Start()
    {
        enemiesToSpawn = new Dictionary<GameObject, List<int>>();
        enemiesToSpawn[enemyPrefabs[0]] = new List<int>{10,5,7,10,12};
        enemiesToSpawn[enemyPrefabs[1]] = new List<int>{0,5,7,10,12};
        enemiesToSpawn[enemyPrefabs[2]] = new List<int>{6,5,7,10,12};
        Debug.Log("GameManager started");
    }
    void Update()
    {
        
    }
    public void NextRound()
    {
        roundNumber++;
    }

    private void SpawnEnemies(int numEnemies, float buffMultiplier)
    {
        
    }
}