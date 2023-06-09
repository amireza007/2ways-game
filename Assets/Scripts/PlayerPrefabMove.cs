using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefabMove : MonoBehaviour
{
    public float speedIncreaseRate = 0.48f;
    public float timer;
    bool isOnTheRightLane = true;
    Transform rightLane;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(rightLane.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //RenderSettings.fogDensity = 100f;
        //important part, could be used for rocks as well!!!
        timer += Time.deltaTime;
        transform.position += new Vector3(0, 0, 0.1f);
        if (timer > speedIncreaseRate)
        {
            //rb.velocity += new Vector3(0, 0, 0.1f);
            
            timer = 0;
        }
        //if (Input.GetKeyDown("right"))
        //Debug.Log(transform.position.x);
        //Debug.Log(rightLane.transform.position.x);
        if (Input.GetKeyDown("t"))
        {
            if (isOnTheRightLane)
            {
                transform.position = new Vector3(-1.4f, transform.position.y, transform.position.z);
                isOnTheRightLane = false;
            }
            else if (!isOnTheRightLane)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                isOnTheRightLane = true;

            }
        }
        if (Input.GetKeyDown("space"))
        {
            //rb.AddForce(0, jumpPower, rb.velocity.z / 10);
        }
    }
}
