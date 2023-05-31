using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laneSpawn: MonoBehaviour
{
    public GameObject player;
    public int spawnRate = 2;
    public double timer = 0d;
    public GameObject lanes;
    float currentSpeed;
    Vector3 offset;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        offset = transform.position - player.transform.position;
        Debug.Log(offset);
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + 10f);
        //currentSpeed = GameObject.FindAnyObjectByType<Rigidbody>(FindObjectOfType)
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;

            //Debug.Log(timer);
        }
        else
        {
            Instantiate(lanes, transform.position, transform.rotation);
            Debug.Log(transform.position);
            timer = 0;
        }
    }
}
