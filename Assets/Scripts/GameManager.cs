using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool godMode = false;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;

    public int scrapCounter = 0;
    [SerializeField] private TextMeshProUGUI scrapText;

    private bool isAlive = true;
    private float textTimer = 0f;
    [Header("UI elements")]
    

    [SerializeField] private GameObject waveObject;
    [SerializeField] private GameObject bossWaveObject;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject deathScreen;

    private PlayerHealth playerHeath;
    private PlayerController playerController;

    [SerializeField] private GameObject pauseScreen;
    [Header("Spawn Locations")]
    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;
    [SerializeField] private Transform spawn3;
    [SerializeField] private Transform spawn4;
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject zeppelinPrefab;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private GameObject boatPrefab1;
    [SerializeField] private GameObject boatPrefab2;
    [Header("Enemies active in scene")]
    [SerializeField] public List<GameObject> enemies;

    private bool gameStarted = true;
    private bool paused = false;


    private int currentWave = 1;
    private bool showWave = true;
    


    private void Start()
    {
        enemies = new List<GameObject>();
        playerController = FindFirstObjectByType<PlayerController>();
        playerHeath = FindFirstObjectByType<PlayerHealth>();    
        DisplayWaveText(true);
        StartWave();

    }

    private void Update()
    {
        EnableGodMode();
        
        DisplayCounters();
        GameOver();
        PauseGame();
        CheckEnemies();

        Debug.Log(textTimer);
        if (showWave)
        {
            ShowText();
        }
    }
    void GameOver()
    {
        if(playerHeath.health <= 0)
        {
            isAlive = false;
            gameStarted = false;
        }
        if (!isAlive)
        {
            deathScreen.SetActive(true);
            Destroy(playerController); 
            if (Input.GetKeyDown(KeyCode.R)) //reset scene
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void StartWave()
    {
        Instantiate(zeppelinPrefab, spawn1.position, Quaternion.identity);
        Instantiate(boatPrefab1 , spawn3.position, Quaternion.identity);
        Instantiate(boatPrefab2 , spawn2.position, Quaternion.identity);
    }

    void CheckEnemies()
    {
        if(enemies.Count == 0)
        {
            BossWave(); 
        }
    }

    void NextWave()
    {
        //code voor volgende wave
    }

    void BossWave()
    {
        bossWaveObject.SetActive(true);
        //code voor boss wave
        Instantiate(zeppelinPrefab, spawn1.position, Quaternion.identity);
        Instantiate(boatPrefab1, spawn3.position, Quaternion.identity);
        Instantiate(boatPrefab2, spawn2.position, Quaternion.identity);
        Instantiate(bossPrefab, spawn4.position, Quaternion.identity);
    }
    void DisplayCounters()
    {
        DisplayScrap();
        DisplayScore();
    }

    void DisplayScrap()
    {
        scrapText.text = "Scrap : " + scrapCounter;
    }
    void DisplayScore()
    {
        scoreText.text = "Score : " + score;
    }

    void DisplayWaveText(bool show)
    {
        waveText.text = "Wave :" + currentWave;
        
    }
    void ShowText()
    {
        waveObject.SetActive(true);
        textTimer += Time.deltaTime;
        if (textTimer > 1)
        {
            textTimer = 0;
            waveObject.SetActive(false);
            showWave = false;
        }
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            if (!paused)
            {
                pauseScreen.SetActive(true);
                PauseAndUnPause(0f, true);
            }
            else
            {
                pauseScreen.SetActive(false);
                PauseAndUnPause(1f, false);
            }
        }
        
    }

    void EnableGodMode()
    {
        if (godMode)
        {
            playerHeath.maxHealth = 10000;
            playerHeath.SetHealth(10000);
        }
    }

    private void PauseAndUnPause(float timeScale, bool isPaused)
    {
        Time.timeScale = timeScale;
        paused = isPaused;
    }
}
