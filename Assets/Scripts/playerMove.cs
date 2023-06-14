using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;


public class playerMove : MonoBehaviour
{
    //PlayerPrefabMove prefabScript;
    //float m_MySliderValue;
    //Animator m_Animator;
    //public GameObject rightLane;
    //public GameObject leftLane;

    //bool isOnTheRightLane = true;

    ////public List<GameObject> torches = new List<GameObject>();
    //public float initialspeed = 100;
    //public float timer = 0;
    //public float speedIncreaseRate = 20;
    //public float jumpPower = 5000;
    //public float torchPower = 4;
    //public float loadScreenSeconds = 2;

    public Rigidbody playerRigidbody;

    //[Range(0f, 1f)] [SerializeField] private float swipeAccuracy;
    //[SerializeField] private float jumpSwipeDegree;

    //private float screenHeight;

    //private Vector2 initialPosition;
    //private Vector2 finalPosition;
    //private bool isSwiping;


    //private void Awake()
    //{
    //    m_Animator = gameObject.GetComponent<Animator>();
    //    screenHeight = Screen.height;
    //}

    void Start()
    {
        
        //below code could be used for restarting from the start of the road
        //Vector3 rightLaneCenter = GameObject.FindWithTag("rightLane").transform.position - (GameObject.FindWithTag("rightLane").transform.localScale / 2);
        //Debug.Log(rightLaneCenter);
        //transform.position = rightLaneCenter + new Vector3(transform.localScale.x/1.2f,2f,0);
        //rb.AddForce(0, 0, initialspeed);

    }

    //private IEnumerator WaitForSceneLoad()
    //{
    //    yield return new WaitForSeconds(loadScreenSeconds);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("LeftLObstacle") || collision.collider.CompareTag("LeftSObstacle") ||
    //        collision.collider.CompareTag("RightSObstacle") || collision.collider.CompareTag("RightLObstacle"))
    //    {
    //        prefabScript.enabled = false;
    //        playerRigidbody.AddForce(-3, 0, -2);
    //        StartCoroutine(WaitForSceneLoad());
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Torch"))
    //    {
    //        Debug.Log(other.gameObject);
    //        RenderSettings.fogEndDistance += torchPower;
    //        Destroy(other.gameObject);
    //    }
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    // Debug.Log(m_Animator.speed);

    //    if (Input.GetKeyDown("space"))
    //    {
    //        PlayerJump();

    //        //m_Animator.speed = 3;

    //        //playerAnimation.ResetTrigger("JumpTrigger");
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        isSwiping = true;
    //        initialPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        finalPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

    //        if (isSwiping && CheckJumpSwipe())
    //        {
    //            PlayerJump();
    //            isSwiping = false;
    //        }
    //    }
    //}

    //private bool CheckJumpSwipe()
    //{
    //    Vector2 swipeVector = finalPosition - initialPosition;
    //    float swipeVectorMagnitude = swipeVector.magnitude;
    //    float swipeSin = swipeVector.y / swipeVectorMagnitude;
        
    //    Debug.Log(swipeSin);

    //    return swipeSin >= Mathf.Sin(Mathf.Deg2Rad * jumpSwipeDegree) &&
    //           swipeVectorMagnitude / screenHeight > swipeAccuracy;
    //}

    //private void PlayerJump()
    //{
    //    playerRigidbody.useGravity = false;

    //    m_Animator.SetTrigger("jump");
    //}

    public void EndJumpAnimation()
    {
        playerRigidbody.useGravity = true;
    }
}