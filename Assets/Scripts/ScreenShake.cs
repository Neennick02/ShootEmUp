using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public bool start = false;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
         Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime/ duration);
            transform.position = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }
        transform.position = startPos;
    }

}
