using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public static GamePlayManager Instance;

    [SerializeField] private GameObject pipePrfab;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private float pipeSpacing = 1f;
    [SerializeField] private float pipeRange = 0.5f;
    [SerializeField] private int pipeThreshold = 20;

    private Dictionary<int, GameObject> createdPipe;
    private int currentPipe;
    private int lastPipe;
    private int score;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        createdPipe = new Dictionary<int, GameObject>();
        currentPipe = 0;
        lastPipe = 0;
        score = 0;
        LevelGenerator();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnScoreInscrese()
    {
        Debug.Log("Current = " + currentPipe + " Last = " + lastPipe);
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
        Vector3 newPos = new Vector3(posX, Random.Range(-pipeRange, pipeRange), 0);
        GameObject pipe = Instantiate(pipePrfab, newPos, Quaternion.identity);
        createdPipe.Add(currentPipe, pipe);
        currentPipe++;
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
        OnScoreInscrese();
    }
    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
