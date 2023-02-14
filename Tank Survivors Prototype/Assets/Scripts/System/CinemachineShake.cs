using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance;

    private CinemachineVirtualCamera vCam;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    float lerpValue;
    float startIntensity;
    float startTime;
    float currentTime;

    private void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        Instance = this;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0, 1 - (currentTime/startTime));
        }
    }

    public void StartShake(float intensity, float time)
    {
        startIntensity = intensity;
        startTime = time;
        currentTime = time;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }
}
