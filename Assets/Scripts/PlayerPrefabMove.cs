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

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(rightLane.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //RenderSettings.fogDensity = 100f;
        //important part, could be used for rocks as well!!!
        timer += Time.deltaTime;
        transform.position += new Vector3(0, 0, 0.009f);

        //transform.position += new Vector3(0, 0, Time.realtimeSinceStartup);
        if (timer > speedIncreaseRate)
        {
            //currentSpeed += Time.deltaTime;
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
        //if (Input.GetKeyDown("right"))
        //Debug.Log(transform.position.x);
        //Debug.Log(rightLane.transform.position.x);

    }
}
