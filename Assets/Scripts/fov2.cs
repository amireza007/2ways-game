using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fov2: MonoBehaviour
{
    // Start is called before the first frame update
    public Light light;
    public Transform player;
    public float disappearRate = 5;
    // Update is called once per frame
    Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position - player.position;
    }
    void Update()
    {
        transform.position = new Vector3(player.position.x + initialPos.x, transform.position.y, player.position.z + initialPos.z);
        //transform.position = Vector3.Lerp(transform.position, player.position + initialPos, 1);

        //light.range -= Time.deltaTime / disappearRate;
    }
}
