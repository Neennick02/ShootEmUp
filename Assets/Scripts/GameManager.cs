using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("GodMode Options")]
    [SerializeField] private bool godMode = false;
    [SerializeField] private float speedupAmount = 2f;
    [SerializeField] private bool unlimitedFire = false;
    public int score = 0;
    public int scrapCounter = 0;
    private bool isAlive = true;
    private float textTimer = 0f;
    [Header("Amount of score before powerups spawns")]
    [SerializeField] private float scoreThreshold;
    [Header("UI elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextDeathScreen;
    [SerializeField] private TextMeshProUGUI scoreTextEndScreen;
    [SerializeField] private TextMeshProUGUI scrapText;
    [SerializeField] private TextMeshProUGUI scrapTextShop;
    [SerializeField] private TextMeshProUGUI scrapTextEndScreen;

    [SerializeField] private GameObject controlsImage;
    [SerializeField] private GameObject waveObject;
    [SerializeField] private GameObject bossWaveObject;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject bossHealthBar;
    public bool openShop = false;
    bool firstWaveStarted = false;
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

    float timer = 0f;

    private void Start()
    {
        Time.timeScale = 1f;
        controlsImage.SetActive(true);
        waveBar = FindFirstObjectByType<WaveBar>();  //links naar andere scripts
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        DisplayWaveText();
    }

    private void Update()
    {
        NextWave();             //checkt of enemies dood zijn
        SpawnPowerUp();         //spawned powerups
        DisplayScrapAndScore(); //UI elementen

        EnableGodMode();        //godmode voor testen
        GameOver();             //speler gaat dood
        EndGame();              //einde spel
        PauseGame();            //pausescherm
        ShowBossHealth();       //laat healthbar van boss zien
    }
    

    

    void StartWave(int waveNumber) //bepaald welke prefab gespawned moet worden
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
                    if (firstWaveStarted) //zorgt dat shop niet 1e wave opent
                    {
                        openShop = false;
                        OpenShop();
                    }
                    showWave = true;  //zorgt dat shop elke andere wave wel opent
                    waveBar.NextWave();
                    DisplayWaveText();
                    StartWave((int)waveBar.currentWave - 1);
                    spawned = true;
                    firstWaveStarted = true;
                }
            }
            if(waveBar.currentWave > 2) //controls verdwijnen uit het scherm
            {
                controlsImage.SetActive(false);
            }
        }
        else
        {
            EndGame(); //eindigt spel na verslaan van boss
        }
       
    }

    void ShowBossHealth() //zorgt dat de healthbar in beeld komt bij begin bosswave
    {
        timer += Time.deltaTime;
        if(waveBar.currentWave == waveBar.maxWaves)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                bossHealthBar.SetActive(true);
            }
        }
    }

    void DisplayScrapAndScore() 
    {
        scrapText.text = "Scrap : " + scrapCounter; //geeft het aantal verzamelde scrap weer
        scrapTextShop.text = "Scrap : " + scrapCounter;
        scrapTextEndScreen.text = "Scrap : " + scrapCounter;
        score = Mathf.Clamp(score, 0, 9999);
        scoreText.text = "Score : " + score;//geeft de score weer
        scoreTextDeathScreen.text = "Score : " + score;
        scoreTextEndScreen.text = "Score : " + score;

    }


    void DisplayWaveText() //geeft de wave tekst weer
    {
        waveText.text = "Wave " + waveBar.currentWave;
        if (waveBar.currentWave == waveBar.maxWaves)
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

    void SpawnPowerUp()
    {
        if(score > scoreThreshold) //spawned random powerup uit array
        {
            RandomPowerup();
            scoreThreshold = scoreThreshold + 100;
        }
    }

    void RandomPowerup() //bepaald welke powerup gespawned moet worden
    {
        float spawnX = Random.Range(-25, 30);
        float spawnY = 53f;
        float spawnZ = 0f;
        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);
            

        int powerupIndex = (int)Random.Range(0, powerups.Count);
        Instantiate(powerups[powerupIndex], spawnPos, Quaternion.identity);
    }

    void OpenShop() //opend shop
    {
        if (!openShop)
        {
            openShop = true;
            PauseAndUnPause(0f, false);
            shopScreen.SetActive(true);
        }
    }

    public void CloseShop() //sluit shop
    {
        PauseAndUnPause(1f, false);
        shopScreen.SetActive(false);
    }

    void EndGame() //eindigt spel
    {
        if (bossBeaten)
        {
            winScreen.SetActive(true);
            Destroy(player.GetComponent<PlayerController>());
            DestroyEnemyTurrets();
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

    public void PauseAndUnPause(float timeScale, bool isPaused) //deel van pauseGame method
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
            playerHealth.maxHealth = 9999;
            playerHealth.SetHealth(9999);
            if (Input.GetKey(KeyCode.Backspace))
            {
                Time.timeScale = speedupAmount;
            }
            else
            {
                //Time.timeScale = 1;
            }
            if (unlimitedFire)
            {
                player.GetComponent<PlayerController>().currentBombRate = 0.1f;
                player.GetComponent<PlayerController>().currentBulletRate = 0.1f;
            }
        }
    }

    public void ResetScene() //methods voor ui knoppen
    {
            SceneManager.LoadScene("MainScene");
    }

    public void QuitToTitle()//methods voor ui knoppen
    {
        SceneManager.LoadScene("HomeScreen");

    }

    public void OpenCredits()//methods voor ui knoppen
    {
        SceneManager.LoadScene("Credits");
    }
    void GameOver() // checkt of speler dood is
    {
        if (playerHealth.health <= 0)
        {
            Time.timeScale = 0f;
            isAlive = false;
            gameStarted = false;
        }
        if (!isAlive)
        {
            deathScreen.SetActive(true);
            Destroy(player.GetComponent<PlayerController>());
            DestroyEnemyTurrets();
        }
    }
}
