using UnityEngine;

public class PowerUpText : MonoBehaviour
{
    private float textTimer = 0;
    protected bool showText = false;
    [SerializeField]
    protected GameObject healthUp, damageUp, fireRateUp, scrapUp;
    
    public void ShowText(bool a, bool b, bool c, bool d)
    {
        showText = true;
        healthUp.SetActive(a);
        damageUp.SetActive(b);
        fireRateUp.SetActive(c);
        scrapUp.SetActive(d);
    } 

    void Update()
    {
        if (showText)
        {
            textTimer += Time.deltaTime;
            if (textTimer > 2)
            {
                healthUp.SetActive(false);
                damageUp.SetActive(false);
                fireRateUp.SetActive(false);
                scrapUp.SetActive(false);
                textTimer = 0;
                showText = false;
            }
        }
    }
}
