using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject healthButton, speedButton, cannonButton, exit;
    [SerializeField] GameObject clickSound;
    [SerializeField] GameObject extraCannon;
    private PlayerHealth playerHealth;
    private GameManager gameManager;
    private PlayerController playerController;
    private HealthBar bar;
    private void Start()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerController = FindFirstObjectByType<PlayerController>();
        gameManager = FindFirstObjectByType<GameManager>();
        bar = FindFirstObjectByType<HealthBar>();
    }
    public void BuyHealth()
    {
        if (gameManager.scrapCounter >= 5)
        {
            Instantiate(clickSound);
            gameManager.scrapCounter -= 5;
            playerHealth.maxHealth = 160;
            bar.SetMaxHealth(playerHealth.maxHealth);
            playerHealth.SetHealth(playerHealth.maxHealth);
            healthButton.SetActive(false);
            gameManager.CloseShop();
        }
    }

    public void BuySpeedUp()
    {
        if (gameManager.scrapCounter >= 10)
        {
            Instantiate(clickSound);
            gameManager.scrapCounter -= 10;
            playerController.movementSpeed = playerController.movementSpeed * 1.6f;
            speedButton.SetActive(false);
            gameManager.CloseShop();
        }
    }

    public void BuyCannonUpgrade()
    {
        if (gameManager.scrapCounter >= 15)
        {
            extraCannon.SetActive(true);
            Instantiate(clickSound);
            gameManager.scrapCounter -= 15;
            cannonButton.SetActive(false);
            playerController.extraCannonActivated = true;
            gameManager.CloseShop();
        }
    }

    public void CloseShop()
    {
        Instantiate(clickSound);
        gameManager.CloseShop();
    }
}
