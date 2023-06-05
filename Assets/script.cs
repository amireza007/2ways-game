using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public float disappearanceSpeed= 2;
    public float timer = 0;
    float end;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogStartDistance = -0.95f;
        end = 24.85f;
        RenderSettings.fogEndDistance = end;
    }

    private void LateUpdate()
    {
        RenderSettings.fogEndDistance -= 10*Time.deltaTime;
    }

    
}
