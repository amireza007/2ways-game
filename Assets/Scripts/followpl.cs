using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followpl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerPrefab;
    //public Transform player;
    Vector3 initialPos;
    void Start()
    {
        initialPos = transform.position - playerPrefab.position;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3 (transform.position.x, transform.position.y, playerPrefab.position.z + initialPos.z);
    }
}
