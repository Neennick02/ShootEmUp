using UnityEngine;

public class DamageUp : PickupScript
{
    public override void Activate()
    {
        bombScript.DamageUp();
        bulletScript.DamageUp();
    }

}
