using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = new Vector3(0, 0, 0);
    }

    public IEnumerator Shake(float shakeMagnitude, float shakeDuration)
    {
        float elapsedShakeTime = 0;

        while (elapsedShakeTime < shakeDuration)
        {
            elapsedShakeTime += Time.deltaTime;
            
            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
