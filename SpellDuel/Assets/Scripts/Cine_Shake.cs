﻿using System.Collections;
using Cinemachine;
using UnityEngine;

public class Cine_Shake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private AnimationCurve curve;
    public Transform followTarget;
    public Transform lookAt;
    private Timers tim;

    private Vector3 origiFollow;
    private Vector3 origiLook;
    public GameObject secondCam;
    public static Cine_Shake Instance { get; private set;}
    private void Awake()
    {
       _virtualCamera = GetComponent<CinemachineVirtualCamera>();
       Instance = this;
       tim = new Timers(1);
    }

    private void OnEnable()
    {
        Stats.OnEnd += DefeatedCinematic;
    }

    private void OnDisable()
    {
        Stats.OnEnd -= DefeatedCinematic;
    }

    public IEnumerator shakeCamera(float intensity, float duration)
    {
        Debug.Log(duration);
        float timer = 0;
        float completeness = 0f;
        if (_virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>())
        {
            CinemachineBasicMultiChannelPerlin cinemachinePerlin = _virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>(); 
            while (completeness<1f)
            {
                timer += Time.deltaTime;
                completeness = timer / duration;
                cinemachinePerlin.m_AmplitudeGain = curve.Evaluate(completeness);
                yield return null;
            }
        }
    }

    private void DefeatedCinematic(Stats stats)
    {
        /*origiFollow = followTarget.position;
        origiLook = lookAt.position;
        Vector3 pos = trans.position + (Vector3.back * 5f)+(Vector3.up*2f);
        followTarget.position = pos;
        lookAt.position = trans.position+Vector3.up;*/
        
            Vector3 pos = Vector3.back * 5f + Vector3.up * 2f + stats.transform.position ;
            secondCam.transform.position = pos;
        
        _virtualCamera.gameObject.SetActive(false);
        secondCam.SetActive(true);
    }
}
