using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public static GamePlayManager Instance;

    [SerializeField] private GameObject pipePrfab;
    [SerializeField] private GameObject moveAblePipePrefab;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private float pipeSpacing = 1f;
    [SerializeField] private float pipeRange = 0.5f;
    [SerializeField] private int pipeThreshold = 20;
    [SerializeField] private int levelExtract = 20;
    [SerializeField] private int foodSpawnChance = 10;

    private Dictionary<int, GameObject> createdPipe;
    private List<GameObject> createdFood;
    private int currentPipe;
    private int lastPipe;
    private int score;
    private float currentMovePipeChance;
 
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        createdFood = new List<GameObject>();
        createdPipe = new Dictionary<int, GameObject>();
        currentPipe = 0;
        lastPipe = 0;
        score = 0;
        currentMovePipeChance = 0;
        LevelGenerator();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnScoreIncrease()
    {
        Debug.Log("Current = " + currentPipe + " Last = " + lastPipe);
        if(score%5 == 0)
        {
            currentMovePipeChance += 5;
        }
        if ((score + pipeThreshold/2) > currentPipe)
        {
            GenerateOnePillar(currentPipe * pipeSpacing);
        }
        if (currentPipe > (lastPipe + pipeThreshold))
        {
            Debug.Log("remove");
            RemoveLastPipe();
        }
    }
    void LevelGenerator()
    {
        for(int i = 0; i < 10; i++)
        {
            GenerateOnePillar(i * pipeSpacing);
        }
    }
    private void GenerateOnePillar(float posX)
    {
        GameObject ob = pipePrfab;
        if(currentMovePipeChance > 100 || Random.Range(0, 100) < currentMovePipeChance)
        {
            ob = moveAblePipePrefab;
        }
        if(Random.Range(0, 100) < foodSpawnChance || true)
        {
            Vector3 foodPos = new Vector3(posX / 2, Random.Range(-pipeRange, pipeRange), 0);
            SpawnFood(foodPos);
        }
        Vector3 newPos = new Vector3(posX, Random.Range(-pipeRange, pipeRange), 0);
        GameObject pipe = Instantiate(ob, newPos, Quaternion.identity);
        createdPipe.Add(currentPipe, pipe);
        currentPipe++;
    }
    private void SpawnFood(Vector3 pos)
    {
        GameObject tmp = Instantiate(foodPrefab, pos, Quaternion.identity);
        createdFood.Add(tmp);
    }
    private void RemoveLastPipe()
    {
        GameObject tmp = createdPipe[lastPipe];
        createdPipe.Remove(lastPipe);
        Destroy(tmp);
        lastPipe++;
    }
    public void AddScore(int amount)
    {
        Debug.Log("score added");
        score += amount;
        OnScoreIncrese();
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
