using UnityEngine;

public class BossMusicScript : MonoBehaviour
{
    [SerializeField] WaveBar waveBar;
    [SerializeField] AudioSource audioSource0;
    [SerializeField] GameObject audioSource1;

    bool started = false;
    void Start()
    {
    }

    void Update()
    {
        if (waveBar.currentWave == waveBar.maxWaves)
        {
            audioSource0.Stop();
            if (!started)
            {
                audioSource1.SetActive(true);
                started = true;
            }
        }
    }
}
