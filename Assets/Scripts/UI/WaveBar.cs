using UnityEngine;

public class WaveBar : MonoBehaviour
{
    [SerializeField] private float width, height;
    private float newWidth = 0;
    [SerializeField] private RectTransform bar;
    
    public float maxWaves = 4;
    public float currentWave = 0;
    void Start()
    {
        newWidth = (currentWave / maxWaves) * width;
        bar.sizeDelta = new Vector2(newWidth, height);
    }

    public void NextWave()
    {
        currentWave++;
        currentWave = Mathf.Clamp(currentWave, 0, maxWaves);
        newWidth = (currentWave / maxWaves) * width;
    }


    void Update()
    {
        bar.sizeDelta = new Vector2(newWidth, height);
    }
}
