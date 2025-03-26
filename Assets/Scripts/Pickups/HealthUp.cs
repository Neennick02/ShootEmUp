using UnityEngine;

public class HealthUp : PickupScript
{
    public override void Activate()
    {
        playerHealth.SetHealth(25);
    }
}
