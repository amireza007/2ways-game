using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fov2: MonoBehaviour
{
    // Start is called before the first frame update
    public Light light;
    public Transform player;
    // Update is called once per frame
    Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position - player.position;
    }
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, player.position + initialPos, 1);
        light.range -= light.range * Time.deltaTime / 20;
    }
}
