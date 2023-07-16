using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    [Header("Spikeballs Spawner Variables")]
    [SerializeField] private List<Transform> spawnPointsStart;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject spikeBall;
    [SerializeField] private int numberOfSpawns;
    [SerializeField] private float timeBetweenSpikeBallSpawnsEasy;
    [SerializeField] private float timeBetweenSpikeBallSpawnsHard;
    private float nextSpikeBallSpawnTime;

    [Header("Environment Spawner Variables")]
    [SerializeField] private Transform[] environmentSpawnPoints;
    [SerializeField] private GameObject[] environmentPrefabs;
    [SerializeField] private float timeBetweenEnvironmentSpawnsMin;
    [SerializeField] private float timeBetweenEnvironmentSpawnsMax;
    private float nextEnvironmentSpawnTime;

    private GameManager gameManager;
    [Header("Difficulty Level Variables")]
    [SerializeField] private float timeUntilMaxDifficulty;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!GameManager.instance.GetIsGameEnden())
        {
            if (Time.time > nextSpikeBallSpawnTime)
            {
                GameManager.instance.AddScore();
                if (GameManager.instance.GetScore() > PlayerPrefs.GetInt("HighScore"))
                {
                    PlayerPrefs.SetInt("HighScore", GameManager.instance.GetScore());
                }

                for (int i = 0; i < numberOfSpawns; i++)
                {
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    GameObject spikeBallGameObject = Instantiate(spikeBall, randomSpawnPoint.position, randomSpawnPoint.rotation);
                    spawnPoints.Remove(randomSpawnPoint);
                }

                spawnPoints.Clear();

                for (int i = 0; i < spawnPointsStart.Count; i++)
                {
                    spawnPoints.Add(spawnPointsStart[i]);
                }

                nextSpikeBallSpawnTime = Time.time + Mathf.Lerp(timeBetweenSpikeBallSpawnsEasy, timeBetweenSpikeBallSpawnsHard, GetDifficultyPercent());
            }
        }

        if (Time.time > nextEnvironmentSpawnTime)
        {
            for (int i = 0; i < environmentSpawnPoints.Length; i++)
            {
                GameObject randomPrefab = environmentPrefabs[Random.Range(0, environmentPrefabs.Length)];
                GameObject environmentGameObject = Instantiate(randomPrefab, environmentSpawnPoints[i].position, environmentSpawnPoints[i].rotation);
            }
            nextEnvironmentSpawnTime = Time.time + Random.Range(timeBetweenEnvironmentSpawnsMin, timeBetweenEnvironmentSpawnsMax);
        }
    }

    public float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / timeUntilMaxDifficulty);
    }
}