using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerPrefabMove : MonoBehaviour
{
    Animator player_animator;
    public float torchPower = 4;
    public float loadScreenSeconds = 2;

    public float speedIncreaseRate = 1f;
    public float timer = 0;
    public float currentSpeed = .05f;
    bool isOnTheRightLane = true;
    Transform rightLane;

    // Start is called before the first frame update
    void Start()
    {
        player_animator = gameObject.GetComponentInChildren<Animator>();
        //transform.position = new Vector3(rightLane.position.x, transform.position.y, transform.position.z);
    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(loadScreenSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("LeftLObstacle") || collision.collider.CompareTag("LeftSObstacle") || collision.collider.CompareTag("RightSObstacle") || collision.collider.CompareTag("RightLObstacle"))
        {
            StartCoroutine(WaitForSceneLoad());
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        //RenderSettings.fogDensity = 100f;
        //important part, could be used for rocks as well!!!
        timer += Time.deltaTime;
        transform.position += new Vector3(0, 0, currentSpeed);

        //transform.position += new Vector3(0, 0, Time.realtimeSinceStartup);
        if (timer > speedIncreaseRate)
        {
            //currentSpeed += Mathf.Pow(Time.deltaTime,2);
            timer = 0;
        }
        if (Input.GetKeyDown("t"))
        {
            if (isOnTheRightLane)
            {
                transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
                isOnTheRightLane = false;
            }
            else if (!isOnTheRightLane)
            {
                //player_animator.SetTrigger("switchLeft");

                transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
                isOnTheRightLane = true;

            }
        }
        //if (Input.GetKeyDown("right"))
        //Debug.Log(transform.position.x);
        //Debug.Log(rightLane.transform.position.x);

    }
}
