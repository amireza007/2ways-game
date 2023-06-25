using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FogController : MonoBehaviour
{
    [SerializeField] private float decreasingMultiplier;
    [SerializeField] private float minFogStartDistance = 10;
    [SerializeField] private float minFogEndDistance = 25;


    private void Update()
    {
        if (RenderSettings.fogStartDistance > minFogStartDistance)
        {
            RenderSettings.fogStartDistance -= Time.deltaTime * decreasingMultiplier;
            RenderSettings.fogEndDistance -= Time.deltaTime * decreasingMultiplier;
        }
    }

}