using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public Transform player;
    public int spawnRate = 2;
    public double timer = 0d;
    public GameObject lanes;
    Vector3 offset;
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        offset = transform.position - player.position;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z);
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        else
        {
            Instantiate(lanes, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
