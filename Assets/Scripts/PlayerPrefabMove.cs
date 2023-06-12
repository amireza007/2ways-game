using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabMove : MonoBehaviour
{
    public float speedIncreaseRate = 1f;
    public float timer = 0;
    public float currentSpeed = .05f;
    bool isOnTheRightLane = true;
    Transform rightLane;

    void Update()
    {
        //RenderSettings.fogDensity = 100f;
        //important part, could be used for rocks as well!!!
        timer += Time.deltaTime;
        transform.position += new Vector3(0, 0, currentSpeed);

        //transform.position += new Vector3(0, 0, Time.realtimeSinceStartup);
        if (timer > speedIncreaseRate)
        {
            currentSpeed += Mathf.Pow(Time.deltaTime,2);
            timer = 0;
        }
        if (Input.GetKeyDown("t"))
        {
            if (isOnTheRightLane)
            {
                transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
                isOnTheRightLane = false;
            }
            else if (!isOnTheRightLane)
            {
                transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
                isOnTheRightLane = true;

            }
        }
        
    }
}
