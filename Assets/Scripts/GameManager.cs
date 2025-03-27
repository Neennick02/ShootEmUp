using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool godMode = false;
    [SerializeField] private bool startBossWave = false;
    public int score = 0;
    public int scrapCounter = 0;
    private bool isAlive = true;
    private float textTimer = 0f;

    [Header("UI elements")]

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scrapText;
    [SerializeField] private GameObject waveObject;
    [SerializeField] private GameObject bossWaveObject;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;

    private PlayerHealth playerHeath;
    private PlayerController playerController;

    [SerializeField] private GameObject pauseScreen;
    [Header("Enemy Waves")]
    [SerializeField] private List<GameObject> waves = new List<GameObject>();
    private int currentWave = 1;

    [Header("Enemies active in scene")]
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] private List<GameObject> powerups = new List<GameObject>();
    
    public bool bossBeaten = false; 

    private bool gameStarted = true;
    private bool paused = false;

    private bool showWave = true;
    


    private void Start()
    {
        bossBeaten = false;
        enemies = new List<GameObject>();

        playerController = FindFirstObjectByType<PlayerController>();
        playerHeath = FindFirstObjectByType<PlayerHealth>();    
        DisplayWaveText(true, false);
        StartWave(0);

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
            DestroyEnemyTurrets();
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

    void StartWave(int waveNumber)
    {
        Instantiate(waves[0], transform.position, Quaternion.identity);
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
            DestroyEnemyTurrets();
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
        StartWave(1);


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

    private void DestroyEnemyTurrets()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].GetComponent<Turret>());
        }
    }

    void EnableGodMode() //geeft heel veel health
    {
        if (godMode)
        {
            playerHeath.maxHealth = 10000;
            playerHeath.SetHealth(10000);
        }
        if (startBossWave)
        {
            enemies.Clear();
            BossWave();
        }
    }
}
