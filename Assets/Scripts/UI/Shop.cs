using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject healthButton, speedButton, cannonButton, exit;
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
            gameManager.CloseShop();
        }
    }

    public void BuySpeedUp()
    {
        if (gameManager.scrapCounter > 5)
        {
            gameManager.scrapCounter -= 5;
            playerController.movementSpeed = playerController.movementSpeed * 1.3f;
            speedButton.SetActive(false);
            gameManager.CloseShop();
        }
    }

    public void BuyCannonUpgrade()
    {
        if (gameManager.scrapCounter > 5)
        {
            gameManager.scrapCounter -= 5;
            cannonButton.SetActive(false);
            gameManager.CloseShop();
            //wordt later toegevoegd
        }
    }
}
