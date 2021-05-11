using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public GameObject pipePrfab;
    public GameObject birdPrefab;
    public GameObject tresschuck;
    public GameObject planeblock;
    public float pipeSpacing = 1f;
    public float pipeRange = 0.5f;
    public float planespace = 5.5f;
    
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
        for(int i = 0; i < 100; i++)
        {
            var pipe = Instantiate(pipePrfab);
            pipe.transform.position = new Vector3(i * pipeSpacing, Random.Range(-pipeRange, pipeRange), 0);
            var tree = Instantiate(tresschuck);
            tree.transform.position = new Vector3(i* Random.Range(-5, 20), -5, 6);
            //plane.transform.position = new Vector3(100, 100, 100);
            var plane = Instantiate(planeblock);
            plane.transform.position = new Vector3(i * planespace, -3.4f, 6.0968f);
            //plane.transform.localScale = new Vector3(1f / 15f, 1, 1f / 5f);
            //Instantiate(plane, new Vector3(i* pipeSpacing, 0, 0), Quaternion.identity, this.transform);
        }
    }
}
