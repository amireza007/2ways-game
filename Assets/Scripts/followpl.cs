using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followpl : MonoBehaviour
{
    public bool shouldFollow = true;
    // Start is called before the first frame update
    public Transform player;
    Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position - player.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (shouldFollow)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, initialPos.z + player.position.z);
        }
    }
}
