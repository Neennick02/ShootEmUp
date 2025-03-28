using UnityEditor;
using UnityEngine;

public class DamageUp : PickupScript
{
    public override void Activate()
    {
        playerController.DamageUp();
        powerUpText.ShowText(false, true, false, false);
    }

}
