using UnityEngine;

public class HealthUp : PickupScript
{
    public override void Activate()
    {
        playerHealth.SetHealth(25);
        powerUpText.ShowText(true, false, false, false);
    }
}
