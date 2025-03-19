using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private bool gameStarted = true;


    private bool paused = false;


    private void Start()
    {
        
    }

    private void Update()
    {
        DisplayScore();
        PauseGame();
    }

    void DisplayScore()
    {
        scoreText.text = "Score :" + score;
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

    private void PauseAndUnPause(float timeScale, bool isPaused)
    {
        Time.timeScale = timeScale;
        paused = isPaused;
    }
}
