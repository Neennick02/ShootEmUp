using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject healthButton, speedButton, cannonButton;
    private PlayerHealth playerHealth;
    private GameManager gameManager;
    private PlayerController playerController;
    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerController = FindFirstObjectByType<PlayerController>();
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void BuyHealth()
    {
        if (gameManager.scrapCounter > 5)
        {
            gameManager.scrapCounter -= 5;
            playerHealth.maxHealth = 160;
            healthButton.SetActive(false);
        }
    }

    public void BuySpeedUp()
    {
        if (gameManager.scrapCounter > 5)
        {
            gameManager.scrapCounter -= 5;
            playerController.movementSpeed = playerController.movementSpeed * 1.3f;
            speedButton.SetActive(false);
        }
    }

    public void BuyCannonUpgrade()
    {
        if (gameManager.scrapCounter > 5)
        {
            gameManager.scrapCounter -= 5;
            cannonButton.SetActive(false);
            //wordt later toegevoegd
        }
    }

    public void CloseShop()
    {
        gameManager.openShop = false;
    }
}
