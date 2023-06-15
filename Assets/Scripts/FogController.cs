using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FogController : MonoBehaviour
{
    [SerializeField] private float decreasingMultiplier;
    [SerializeField] private float initialFogStartDistance = 20;
    [SerializeField] private float initialFogEndDistance = 30;


    private void Awake()
    {
        RenderSettings.fogStartDistance = initialFogStartDistance;
        RenderSettings.fogEndDistance = initialFogEndDistance;
    }

    private void Update()
    {
        RenderSettings.fogStartDistance -= Time.deltaTime * decreasingMultiplier;
        RenderSettings.fogEndDistance -= Time.deltaTime * decreasingMultiplier;
    }

    private void LateUpdate()
    {
    }
}