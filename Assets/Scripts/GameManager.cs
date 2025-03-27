using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool godMode = false;
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
    [SerializeField] private GameObject pauseScreen;

    [Header("Enemy Waves")]
    [SerializeField] private List<GameObject> waves = new List<GameObject>();
    private WaveBar waveBar;

    [Header("Enemies active in scene")]
    [SerializeField] public List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> powerups = new List<GameObject>();
    [Header("Player")]
    [SerializeField] private GameObject player;
    private PlayerHealth playerHealth;

    public bool bossBeaten = false; 
    private bool gameStarted = true;
    private bool paused = false;
    private bool showWave = true;
    


    private void Start()
    {
        waveBar = FindFirstObjectByType<WaveBar>();  //links naar andere scripts
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        DisplayWaveText();
    }

    private void Update()
    {


        NextWave();

        DisplayScrapAndScore(); //UI elementen

        
        EnableGodMode();
        GameOver();
        EndGame();
        PauseGame();
    }
    

    

    void StartWave(int waveNumber)
    {
        Instantiate(waves[waveNumber], transform.position, Quaternion.identity);
    }

    

    void NextWave()
    {
        //code voor volgende wave
        bool spawned = false;

        if (!bossBeaten)
        {
            if (showWave) //als showWave true is - komt text in beeld
            {
                ShowText();
            }
            if (enemies.Count == 0)//checkt of alle enemys dood zijn
            {
                if (!spawned)
                {
                    DisplayWaveText();
                    showWave = true;
                    StartWave((int)waveBar.currentWave);
                    waveBar.NextWave();
                    spawned = true;
                }
            }
        }
       
    }

    void DisplayScrapAndScore() 
    {
        scrapText.text = "Scrap : " + scrapCounter; //geeft het aantal verzamelde scrap weer
        score = Mathf.Clamp(score, 0, 9999);
        scoreText.text = "Score : " + score;//geeft de score weer
    }


    void DisplayWaveText() //geeft de wave tekst weer
    {
        waveText.text = "Wave :" + waveBar.currentWave;
        if (waveBar.currentWave == waveBar.maxWaves )
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

    void EndGame()
    {
        if (bossBeaten)
        {
            winScreen.SetActive(true);
            Destroy(player.GetComponent<PlayerController>());
            
            DestroyEnemyTurrets();
            ResetScene();
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
            playerHealth.maxHealth = 1000;
            playerHealth.SetHealth(10000);
        }
    }

    private void ResetScene()
    {
        if (Input.GetKeyDown(KeyCode.R)) //reset scene
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void GameOver() // checkt of speler dood is
    {
        if (playerHealth.health <= 0)
        {
            isAlive = false;
            gameStarted = false;
        }
        if (!isAlive)
        {
            deathScreen.SetActive(true);
            Destroy(player.GetComponent<PlayerController>());
            DestroyEnemyTurrets();
            ResetScene();
        }
    }
}
