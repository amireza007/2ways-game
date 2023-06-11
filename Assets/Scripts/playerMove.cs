using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;


public class playerMove : MonoBehaviour
{
    float m_MySliderValue;
    Animator m_Animator;
    public GameObject rightLane;
    public GameObject leftLane;
    bool isOnTheRightLane = true;
    //public List<GameObject> torches = new List<GameObject>();
    public float initialspeed = 100;
    public float timer = 0;
    public float speedIncreaseRate = 20;
    public float jumpPower = 5000;
    public float torchPower = 4;
    public float loadScreenSeconds = 2;

    public Rigidbody playerRigidbody;
    // Start is called before the first frame update

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        //below code could be used for restarting from the start of the road
        //Vector3 rightLaneCenter = GameObject.FindWithTag("rightLane").transform.position - (GameObject.FindWithTag("rightLane").transform.localScale / 2);
        //Debug.Log(rightLaneCenter);
        //transform.position = rightLaneCenter + new Vector3(transform.localScale.x/1.2f,2f,0);
        //rb.AddForce(0, 0, initialspeed);

        Physics.gravity = new Vector3(0, -20f, 0);
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
        Debug.Log(m_Animator.speed);

        if (Input.GetKeyDown("space"))
        {
            playerRigidbody.useGravity = false;

            m_Animator.SetTrigger("jump");

            //m_Animator.speed = 3;

            //playerAnimation.ResetTrigger("JumpTrigger");
        }
    }

    public void EndJumpAnimation()
    {
        playerRigidbody.useGravity = false;
    }
    //void OnGUI()
    //{
    //    //Create a Label in Game view for the Slider
    //    GUI.Label(new Rect(0, 25, 40, 60), "Speed");
    //    //Create a horizontal Slider to control the speed of the Animator. Drag the slider to 1 for normal speed.

    //    m_MySliderValue = GUI.HorizontalSlider(new Rect(45, 25, 300, 100), m_MySliderValue, 0.0F, 1.0F);
    //    //Make the speed of the Animator match the Slider value
    //    m_Animator.speed = m_MySliderValue;
    //}
}
