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
    [SerializeField] private GameObject winScreen;

    private PlayerHealth playerHeath;
    private PlayerController playerController;

    [SerializeField] private GameObject pauseScreen;
    [Header("Spawn Locations")]
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>(); //0-3 zijn air spawnpoint, 4-7 zijn water spawns
    
    [Header("Enemy Prefabs")]
    [SerializeField] private List<GameObject> enemiePrefabs = new List<GameObject>(); 

    [Header("Enemies active in scene")]
    [SerializeField] public List<GameObject> enemies;
    public bool bossBeaten = false; 

    private bool gameStarted = true;
    private bool paused = false;


    private int currentWave = 1;
    private bool showWave = true;
    


    private void Start()
    {
        bossBeaten = false;
        enemies = new List<GameObject>();

        playerController = FindFirstObjectByType<PlayerController>();
        playerHeath = FindFirstObjectByType<PlayerHealth>();    
        DisplayWaveText(true, false);
        StartWave();

    }

    private void Update()
    {
        EnableGodMode();
        
        DisplayCounters();

        GameOver();
        EndGame();
        PauseGame();

        CheckEnemies();

        if (showWave)
        {
            ShowText();
        }
    }
    void GameOver() // checkt of speler dood is
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
            ResetScene();
        }
    }

    private void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.R)) //reset scene
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void StartWave()
    {
        Instantiate(enemiePrefabs[0], spawnPoints[5].position, Quaternion.identity);
        Instantiate(enemiePrefabs[1] , spawnPoints[4].position, Quaternion.identity);
        Instantiate(enemiePrefabs[2] , spawnPoints[3].position, Quaternion.identity);
    }

    void CheckEnemies()
    {
        if(enemies.Count == 0 && !bossBeaten)
        {
            BossWave(); 
        }
    }

    void EndGame()
    {
        if (bossBeaten)
        {
            winScreen.SetActive(true);
            Destroy(playerController);
            ResetScene();
        }
    }

    void NextWave()
    {
        //code voor volgende wave
    }

    void BossWave()
    {
        //code voor boss wave
        showWave = true;
        DisplayWaveText(true, true);
        if (showWave)
        {
            ShowText();
        }
        //enemise die gespawned worden
        Instantiate(enemiePrefabs[0], spawnPoints[5].position, Quaternion.identity);
        Instantiate(enemiePrefabs[1], spawnPoints[4].position, Quaternion.identity);
        Instantiate(enemiePrefabs[2], spawnPoints[0].position, Quaternion.identity);
        Instantiate(enemiePrefabs[3], spawnPoints[3].position, Quaternion.identity);


    }
    void DisplayCounters()
    {
        DisplayScrap();
        DisplayScore();
        //wave bar
    }

    void DisplayScrap() //geeft het aantal verzamelde scrap weer
    {
        scrapText.text = "Scrap : " + scrapCounter;
    }
    void DisplayScore() //geeft de score weer
    {
        scoreText.text = "Score : " + score;
    }

    void DisplayWaveText(bool show, bool isBoss) //geeft de wave tekst weer
    {
        waveText.text = "Wave :" + currentWave;
        if (isBoss)
        {
            waveText.text = "Boss incoming!";
        }
    }
    void ShowText() //kan tekst tijdelijk in beeld brengen
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

    private void PauseGame() //zorgt voor pauze functionaliteit
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

    private void PauseAndUnPause(float timeScale, bool isPaused) //deel van pauseGame method
    {
        Time.timeScale = timeScale;
        paused = isPaused;
    }

    void EnableGodMode() //geeft heel veel health
    {
        if (godMode)
        {
            playerHeath.maxHealth = 10000;
            playerHeath.SetHealth(10000);
        }
    }
}
