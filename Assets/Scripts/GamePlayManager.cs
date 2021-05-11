using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Random = UnityEngine.Random;

public class GamePlayManager  : MonoBehaviour
{
    public static GamePlayManager Instance;

    [SerializeField] private GameObject pipePrfab;
    [SerializeField] private GameObject moveAblePipePrefab;
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private GameObject tresschuck;
    [SerializeField] private GameObject planeblock;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform pipeHolder;
    [SerializeField] private Transform decorateHolder;
    [SerializeField] private Transform foodHolder;
    [SerializeField] private float pipeSpacing = 1f;
    [SerializeField] private float pipeRange = 0.5f;
    [SerializeField] private float planespace = 5.5f;
    [SerializeField] private int pipeThreshold = 20;
    [SerializeField] private int levelExtract = 20;
    [SerializeField] private int foodSpawnChance = 10;
    [SerializeField] private List<GameObject> foodPrefabs;
    [SerializeField] private GameObject pauseCanvasObject;
    [SerializeField] private GameObject loseCanvasObject;

    private Dictionary<int, GameObject> createdPipe;
    private List<GameObject> createdFood;
    private int currentPipe;
    private int lastPipe;
    private int score;
    private float currentMovePipeChance;
    private bool isPause;
    private bool isEnd;
 
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
        isPause = false;
        isEnd = false;
        pauseCanvasObject.SetActive(isPause);
        loseCanvasObject.SetActive(isEnd);
        LevelGenerator();
        GamePauseManager.instance.ResumeGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isEnd == false)
        {
            if (isPause)
            {
                GamePauseManager.instance.ResumeGame();
            }
            else
            {
                GamePauseManager.instance.PauseGame();
            }
            isPause = !isPause;
            pauseCanvasObject.SetActive(isPause);
        }
    }
    public void DisplayText()
    {
        scoreText.text = score.ToString();
    }
    public void OnScoreIncrease()
    {
        DisplayText();
        if (score%5 == 0)
        {
            currentMovePipeChance += 5;
        }
        if ((score + pipeThreshold/2) > currentPipe)
        {
            GenerateNewPillar(currentPipe * pipeSpacing);
        }
        if (currentPipe > (lastPipe + pipeThreshold))
        {
            RemoveLastPipe();
        }
    }
    void LevelGenerator()
    {
        for(int i = 0; i < 10; i++)
        {
            GenerateNewPillar(i * pipeSpacing);
        }
    }
    private void GenerateNewPillar(float posX)
    {
        GameObject ob = pipePrfab;
        if(currentMovePipeChance > 100 || Random.Range(0, 100) < currentMovePipeChance)
        {
            ob = moveAblePipePrefab;
        }
        if(Random.Range(1, 100) < foodSpawnChance)
        {
            Vector3 foodPos = new Vector3(posX - (pipeSpacing/2), Random.Range(-pipeRange, pipeRange), 0);
            SpawnFood(foodPos);
        }
        Vector3 newPos = new Vector3(posX, Random.Range(-pipeRange, pipeRange), 0);
        GameObject pipe = Instantiate(ob, newPos, Quaternion.identity, pipeHolder);
        GameObject tree = Instantiate(tresschuck, decorateHolder);
        tree.transform.position = new Vector3(posX, -5, 6);
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
    private void SpawnFood(Vector3 pos)
    {
        GameObject tmp = Instantiate(foodPrefabs[Random.Range(0,foodPrefabs.Count)], pos, Quaternion.identity, foodHolder);
        createdFood.Add(tmp);
    }
    public void DeleteFood(FoodController food)
    {
        GameObject tmp = food.gameObject;
        if (createdFood.Contains(tmp))
        {
            createdFood.Remove(tmp);
        }
        Destroy(tmp);
    }
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreIncrease();
    }
    public void GameOver()
    {
        if (isEnd == true) return;
        if(PlayerPrefs.GetInt("HighScore", 0) < score )
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        SFXController.Instance.PlayFail();

        GamePauseManager.instance.PauseGame();
        isEnd = true;
        loseCanvasObject.SetActive(isEnd);
        GameObject.Find("High Score Text").GetComponent<UnityEngine.UI.Text>().text = "Your High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single) ;
    }

    public void MainMemu()
    {

        SceneManager.LoadScene("HomeScene");
    }
}
