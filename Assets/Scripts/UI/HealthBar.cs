using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] float width, height;
    private float maxhealth;
    private float health;

    [SerializeField] RectTransform healthBar;
    public void SetMaxHealth(float max) //steld de maximale health in
    {
        maxhealth = max;
    }

    public void SetHealth(float Health) //veranderd health bij nemen van damage
    {
        health = Health;
        float newWidth = (health / maxhealth) * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
}
