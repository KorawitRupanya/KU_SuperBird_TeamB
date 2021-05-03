using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public GameObject pipePrfab;
    public GameObject birdPrefab;
    public float pipeSpacing = 1f;
    public float pipeRange = 0.5f;
    
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
        for(int i = 0; i < 10; i++)
        {
            var pipe = Instantiate(pipePrfab);
            pipe.transform.position = new Vector3(i * pipeSpacing, Random.Range(-pipeRange, pipeRange), 0);
        }
    }
}
