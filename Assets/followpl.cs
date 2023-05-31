using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followpl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position - player.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3 (transform.position.x, transform.position.y, player.position.z + initialPos.z);
    }
}
