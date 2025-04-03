using UnityEngine;

public class FirerateUp : PickupScript
{
    public override void Activate()
    {
        playerController.FireRateUp();
        powerUpText.ShowText(false, false, true, false);
    }
}
