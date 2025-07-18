﻿using System.Collections;
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

    protected int currentLevel = 1;
    private float baseEnemySpeed = 3f;

    AudioManager audioManager;


    void Awake()
    {
        StartLevel();
    }



    void StartLevel()
    {
        // Reset player position
        player.transform.position = playerStartPoint.position;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.whistle);

        // Clear existing enemies
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Lineman"))
            Destroy(obj);

        // Spawn enemies
        int enemiesToSpawn = Mathf.Min(currentLevel, spawnPoints.Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
            enemy.GetComponent<EnemyChase>().speed = baseEnemySpeed + (currentLevel * 0.2f);
            enemy.transform.Rotate(0, 0, 180);
        }

        // Spawn lineman every 3rd level
        if (currentLevel % 3 == 0)
        {
            int linemanIndex = enemiesToSpawn % spawnPoints.Length;
            Instantiate(linemanPrefab, spawnPoints[linemanIndex].position, Quaternion.identity);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
        Debug.Log("Current Level: " + currentLevel);
        ScoreManager.instance.addScore();

        // Delay level start slightly to let the whistle play
        StartCoroutine(DelayedStartLevel());
    }

    private IEnumerator DelayedStartLevel()
    {
        yield return new WaitForSecondsRealtime(0.25f); // Give time for whistle
        StartLevel();
    }


    public int getCurrentLevel()
    {
        return currentLevel;
    }
}
