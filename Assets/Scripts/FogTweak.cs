using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public float disappearanceSpeed= 10;
    public float timer = 0;
    //float end;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fogStartDistance = -0.95f;
        RenderSettings.fogEndDistance = 30.85f;
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        if(timer > disappearanceSpeed)
        {
            if(RenderSettings.fogEndDistance > RenderSettings.fogStartDistance + 4f)
            {
                // Debug.Log(RenderSettings.fogEndDistance - RenderSettings.fogStartDistance);
                RenderSettings.fogEndDistance -= Time.deltaTime / 1.4f;
            }
        }
    }

    
}
