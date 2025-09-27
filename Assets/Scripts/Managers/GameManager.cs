using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // needs to be implemented


    private bool endlessEnabled = false;
    private int roundNumber = 0;
    private int enemiesRemaining = 0;
    private Dictionary<Enemy, List<int>> enemiesToSpawn;
    private int enemiesKilled = 0;
    [SerializeField] private int roundDuration = 20; // seconds
    [SerializeField] private float spawnInterval = 2f; // seconds
    [SerializeField] private List<Enemy> enemyPrefabs;
    private Timer gameTimer;
    public static GameManager Instance { get; private set; }
    Enemy enemy;
    private void Awake()
    {
        gameTimer = new Timer(roundDuration);
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        BuffButton[] buttons = FindObjectsByType<BuffButton>(FindObjectsSortMode.None);
        foreach (BuffButton button in buttons)
        {
            button.DisallowPress();
        }
    }
    void Start()
    {

        enemiesToSpawn = new Dictionary<Enemy, List<int>>();
        enemiesToSpawn[enemyPrefabs[0]] = new List<int> { 10, 5, 7, 10, 12 };
        enemiesToSpawn[enemyPrefabs[1]] = new List<int> { 0, 5, 7, 10, 12 };
        enemiesToSpawn[enemyPrefabs[2]] = new List<int> { 6, 5, 7, 10, 12 };
        Debug.Log("GameManager Start");
        gameTimer.Start();
        for (int i = 0; i < enemyPrefabs.Count; i++)
            SpawnEnemies(enemiesToSpawn[enemyPrefabs[i]][roundNumber], i);
    }
    void Update()
    {
        Debug.Log("GameManager Update");
        gameTimer.Update();
        if (gameTimer.IsFinished)
        {
            NextRound();
        }
        foreach (Enemy enemy in FindObjectsByType<Enemy>(FindObjectsSortMode.None))
            enemy.Follow(Player.Instance.getTransform());
    }
    public void NextRound()
    {
        gameTimer.Start();
        roundNumber++;
        for (int i = 0; i < enemyPrefabs.Count; i++)
            SpawnEnemies(enemiesToSpawn[enemyPrefabs[i]][roundNumber], i);
    }
    private void SpawnEnemy(Enemy enemy, Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
    }
    private void SpawnEnemies(int numEnemies, int enemyIndex)
    {
        Vector3 playerPos = Player.Instance.getTransform().transform.position;
        for (int i = 0; i < numEnemies; i++)
        {
            Debug.Log("Spawning enemy " + enemyIndex);
            enemy = Instantiate(enemyPrefabs[enemyIndex], new Vector3(playerPos.x + Random.Range(-20, -8), playerPos.y + Random.Range(8, 20), 0), Quaternion.identity);
        }
    }
}