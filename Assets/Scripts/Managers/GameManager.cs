using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private boolean endlessEnabled = 0;
    private int roundNumber = 0;
    private int enemiesRemaining = 0;
    [SerializeField] private int enemiesToSpawn = 5;
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