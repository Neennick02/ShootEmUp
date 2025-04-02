using UnityEngine;

public class RandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundArray;

    private AudioSource audioSource;

    private float destroytimer = 0f;


    private void Awake()
    {
        //checkt of er een AudioSource GameObject is
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        PlayeSound();
    }

    private void Update()
    {
        //zorgt dat gameobject zichzelf destroyd
        destroytimer += Time.deltaTime;
        if (destroytimer > 2f)
        {
            Destroy(gameObject);
        }
    }

    public void PlayeSound()
    {
        //assigned een random clip uit de array
        AudioClip clip = soundArray[UnityEngine.Random.Range(0, soundArray.Length)];
        //speelt clip af op audioSource
        audioSource.PlayOneShot(clip);
    }
}
