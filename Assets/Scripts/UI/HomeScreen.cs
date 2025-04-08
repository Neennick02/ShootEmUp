using UnityEngine;
using UnityEngine.SceneManagement;
public class HomeScreen : MonoBehaviour
{
    [SerializeField] GameObject clickSound;
    public void StartGame()
    {
        Instantiate(clickSound);
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnHome()
    {
        Instantiate(clickSound);
        SceneManager.LoadScene("HomeScreen");
    }

    public void QuitGame()
    {
        Instantiate(clickSound);
        Application.Quit();
        Debug.Log("Quit");
    }
}
