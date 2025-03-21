using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool godMode = false;
    public int score = 0;

    [Header("Do Not Touch")]
    private float textTimer = 0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject waveObject;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private PlayerHealth playerHeath;

    [SerializeField] private Transform spawn1, spawn2, spawn3, spawn4;

    [SerializeField] private GameObject zeppelinPrefab, boatPrefab1, boatPrefab2;
    private bool gameStarted = true;


    private bool paused = false;
    private int currentWave = 1;
    private List<GameObject> enemies;


    private void Start()
    {
        enemies = new List<GameObject>();
        StartWave();
    }

    private void Update()
    {
        textTimer += Time.deltaTime;
        Debug.Log(textTimer);
        EnableGodMode();
        DisplayWaveText();
        DisplayScore();
        PauseGame();
    }

    void StartWave()
    {
        Instantiate(zeppelinPrefab, spawn1.position, Quaternion.identity);
        Instantiate(boatPrefab1 , spawn3.position, Quaternion.identity);
        Instantiate(boatPrefab2 , spawn2.position, Quaternion.identity);
    }
    void DisplayScore()
    {
        scoreText.text = "Score :" + score;
    }

    void DisplayWaveText()
    {
        waveText.text = "Wave :" + currentWave; 
        ShowText(waveObject, 1f);
    }
    void ShowText(GameObject obj, float duration)
    {
        obj.SetActive(true);
        if(textTimer > duration)
        {
            obj.SetActive(false);
        }
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
            playerHeath.maxHealth = 10000f;
            playerHeath.SetHealth(10000f);
        }
    }

    private void PauseAndUnPause(float timeScale, bool isPaused)
    {
        Time.timeScale = timeScale;
        paused = isPaused;
    }
}
