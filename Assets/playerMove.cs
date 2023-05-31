using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject rightLane;
    public GameObject leftLane;
 
    public float speedrate = 30;
    float timer = 0;
    // Start is called before the first frame update

    void Start()
    {
        //below code could be used for restarting from the start of the road
        Vector3 lo = GameObject.FindWithTag("rightLane").transform.position - (GameObject.FindWithTag("rightLane").transform.localScale / 2);
        Debug.Log(lo);
        transform.position = lo + new Vector3(transform.localScale.x/1.2f,2f,0);
        rb.AddForce(0, 0, speedrate);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > speedrate)
        {
            rb.AddForce(0, 0, speedrate);
            timer = 0;
        }
        if (Input.GetKeyDown("right"))
        {
            transform.position = new Vector3(rightLane.transform.position.x, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown("left"))
        {
            transform.position = new Vector3(leftLane.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
