using Unity.Cinemachine;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingShakeIntensity;

    void Start()
    {
        perlin = gameObject.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            perlin.AmplitudeGain = Mathf.Lerp(startingShakeIntensity, 0f, (1 - (shakeTimer / shakeTimerTotal)));
        }
    }

    public void ShakeCamera(float shakeIntensity, float shakeDuration)
    {
        perlin.AmplitudeGain = shakeIntensity;
        
        startingShakeIntensity =  shakeIntensity;
        shakeTimerTotal = shakeDuration;
        shakeTimer = shakeDuration;
    }
}
