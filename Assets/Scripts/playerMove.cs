using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;


public class playerMove : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject rightLane;
    public GameObject leftLane;
    bool isOnTheRightLane = true;
    //public List<GameObject> torches = new List<GameObject>();
    public float initialspeed = 100;
    public float timer = 0;
    public float speedIncreaseRate = 20;
    public float jumpPower = 5000;
    public float torchPower = 4;
    // Start is called before the first frame update

    void Start()
    {
        //below code could be used for restarting from the start of the road
        //Vector3 rightLaneCenter = GameObject.FindWithTag("rightLane").transform.position - (GameObject.FindWithTag("rightLane").transform.localScale / 2);
        //Debug.Log(rightLaneCenter);
        //transform.position = rightLaneCenter + new Vector3(transform.localScale.x/1.2f,2f,0);
        //rb.AddForce(0, 0, initialspeed);

        Physics.gravity = new Vector3(0, -20f, 0);
    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Rock")) {
            GameObject.FindGameObjectWithTag("PlayerPrefab").GetComponent<PlayerPrefabMove>().enabled = false;
            //mainCamera.transform.position = transform.position;
            //WaitForSecondsRealtime s = new WaitForSecondsRealtime(3);
            StartCoroutine(WaitForSceneLoad());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Torch"))
        {
            Debug.Log(other.gameObject);
            RenderSettings.fogEndDistance += torchPower;
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, 0, 0.1f);

    }
}
