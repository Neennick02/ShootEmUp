using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] float width, height;
    private float maxhealth;
    private float health;

    [SerializeField] RectTransform healthBar;

    private void Start()
    {

    }

    public void SetMaxHealth(float max)
    {
        maxhealth = max;
    }

    public void SetHealth(float Health)
    {
        health = Health;
        float newWidth = (health / maxhealth) * width;

        healthBar.sizeDelta = new Vector2(newWidth, height);
    }
}
