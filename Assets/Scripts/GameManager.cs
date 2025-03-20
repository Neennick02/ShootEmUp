using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool godMode = false;
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Health playerHeath;
    private bool gameStarted = true;


    private bool paused = false;
    private int currentWave = 1;
    private List<GameObject> enemies;


    private void Start()
    {
        enemies = new List<GameObject>();
        EnableGodMode();
    }

    private void Update()
    {
        DisplayUI();
        PauseGame();
    }

    void DisplayUI()
    {
        //alle ui elementen die door de gamemanager aangestuurd worden
        DisplayScore();
        DisplayHealth();
    }

    void DisplayScore()
    {
        scoreText.text = "Score :" + score;
    }

    void DisplayHealth()
    {
       healthText.text = "Health :" + playerHeath.currentHealth;
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            if (!paused)
            {
                PauseAndUnPause(0f, true);
            }
            else
            {
                PauseAndUnPause(1f, false);
            }
        }
        
    }

    void EnableGodMode()
    {
        if (godMode)
        {
            playerHeath.currentHealth = 1000000;
        }
    }

    private void PauseAndUnPause(float timeScale, bool isPaused)
    {
        Time.timeScale = timeScale;
        paused = isPaused;
    }
}
