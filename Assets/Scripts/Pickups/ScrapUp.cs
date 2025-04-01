using UnityEngine;

public class ScrapUp : PickupScript
{
    [SerializeField] private int scrapAmount = 5;
    public override void Activate()
    {
        gameManager.scrapCounter += scrapAmount;
        powerUpText.ShowText(false, false, false, true);
    }
}
