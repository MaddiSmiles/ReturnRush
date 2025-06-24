using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject linemanPrefab;
    public Transform[] spawnPoints;
    public Transform playerStartPoint;
    public GameObject player;

    private int currentLevel = 1;
    private float baseEnemySpeed = 3f;

    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        // Move player back to start
        player.transform.position = playerStartPoint.position;

        // Clear existing enemies (if respawning)
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Lineman"))
            Destroy(obj);

        // Spawn enemies for this level
        int enemiesToSpawn = Mathf.Min(currentLevel, spawnPoints.Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
            enemy.GetComponent<EnemyChase>().speed = baseEnemySpeed + (currentLevel * 0.2f); // Increase speed slightly
        }

        // Optional: spawn a lineman every 3rd level
        if (currentLevel % 3 == 0)
        {
            int linemanIndex = enemiesToSpawn % spawnPoints.Length;
            Instantiate(linemanPrefab, spawnPoints[linemanIndex].position, Quaternion.identity);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        StartLevel();
    }
}
