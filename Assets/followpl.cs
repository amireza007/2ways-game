using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
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

        transform.position = Vector3.Lerp(transform.position, player.position + initialPos, 1);
    }
}
