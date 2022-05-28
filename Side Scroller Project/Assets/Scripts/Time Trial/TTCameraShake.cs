using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTCameraShake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve curve;
    public float duration = 1f;
    Vector3 initialPosition;
    public float strength = 1;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (start)
        {
            ContinuousShaking();
        }
    }

    void ContinuousShaking()
    {
        transform.position = initialPosition + Random.insideUnitSphere * strength;
    }

    IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = curve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}
