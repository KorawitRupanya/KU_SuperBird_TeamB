using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public GameObject pipePrefab;
    public GameObject moveAblePipePrefab;
    public GameObject birdPrefab;
    public float pipeSpacing = 2f;
    public float pipeRange = 1f;
    public int levelExtract = 20;
    
    void Start()
    {
        LevelGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LevelGenerator()
    {
        for(int i = 0; i < 30; i++)
        {

            GameObject pipe;
            if (i >= levelExtract)
            {
                pipe = Instantiate(moveAblePipePrefab);
            }
            else
            {
                pipe = Instantiate(pipePrefab);
            }
            pipe.transform.position = new Vector3(i * pipeSpacing, Random.Range(-pipeRange, pipeRange), 0);
        }
    }
}
