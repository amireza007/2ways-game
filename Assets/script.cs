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
    private void OnRenderObject()
    {
        timer = -Time.deltaTime * 10f;
        RenderSettings.fogEndDistance -= 1;

        //RenderSettings.fogEndDistance -= 0.1f;
        Debug.Log("This has been called");
        if (timer < -disappearanceSpeed)
        {
            Debug.Log(RenderSettings.fogStartDistance <= RenderSettings.fogEndDistance);
            if (RenderSettings.fogStartDistance <= RenderSettings.fogEndDistance)
            {
                RenderSettings.fogEndDistance -= 1;
            }
            timer = 0;
        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
        OnRenderObject();
    }
}
