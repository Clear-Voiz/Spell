using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using UnityEngine;

public class Cine_Shake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private AnimationCurve curve;
    public static Cine_Shake Instance { get; private set;}
    private void Awake()
    {
       _virtualCamera = GetComponent<CinemachineVirtualCamera>();
       Debug.Log(_virtualCamera);
       Instance = this;
    }

    public IEnumerator shakeCamera(float intensity, float duration)
    {
        bool activate = true;
        float timer = 0;
        float completeness;
        if (_virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>())
        {
            CinemachineBasicMultiChannelPerlin cinemachinePerlin = _virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>(); 
            while (activate)
            {
                timer += Time.deltaTime;
                completeness = timer / duration;
                cinemachinePerlin.m_AmplitudeGain = curve.Evaluate(completeness);
                if (completeness>=1f) break;
                yield return null;
            }
        }
        else
        {
            Debug.Log("no existe");
            activate = false;
        }

        activate = false;
    }
}
