using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class PlayerPrefabMove : MonoBehaviour
{
    followpl followPlayerScript;
    public float speedIncreaseRate = 1f;
    public float timer = 0;
    public float currentSpeed = .05f;
    bool isOnTheRightLane = true;
    Transform rightLane;
    PlayerPrefabMove prefabScript;
    float m_MySliderValue;
    Animator m_Animator;

    private float rotateRateMultiplier = 1;
    [SerializeField] private float decreaseRotateRate;


    //public List<GameObject> torches = new List<GameObject>();
    public float initialspeed = 100;
    public float jumpPower = 5000;
    public float torchPower = 4;
    public float loadScreenSeconds = 2;

    public Rigidbody playerRigidbody;

    [Range(0f, 1f)] [SerializeField] private float swipeAccuracy;
    [SerializeField] private float jumpSwipeDegree;

    private float screenHeight;

    private Vector2 initialPosition;
    private Vector2 finalPosition;
    private bool isSwiping;
    private void Awake()
    {
        m_Animator = this.GetComponentInChildren<Animator>();
        screenHeight = Screen.height;
    }

    private void Start()
    {
        //Physics.gravity = new Vector3(0, -20f, 0);

    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(loadScreenSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {

            //prefabScript.enabled = false;
            followPlayerScript = GameObject.FindGameObjectWithTag("CameraMover").GetComponent<followpl>();
            followPlayerScript.enabled = false;
            //0.5f, 1f, 0.5f, 1f)
            CameraShaker.Instance.ShakeOnce(0.4f, 4f, 0.5f, 2f);
            this.enabled = false;

            StartCoroutine(StopRotating());

            playerRigidbody.AddForce(-80, 0, -50);

            StartCoroutine(WaitForSceneLoad());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    IEnumerator StopRotating()
    {
        while (rotateRateMultiplier > 0)
        {
            rotateRateMultiplier -= decreaseRotateRate;
            m_Animator.SetFloat("RotateRate", rotateRateMultiplier);

            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += new Vector3(0, 0, currentSpeed);

        //transform.position += new Vector3(0, 0, Time.realtimeSinceStartup);
        if (timer > speedIncreaseRate)
        {
            currentSpeed += Mathf.Pow(Time.deltaTime, 2);
            timer = 0;
        }
        if (Input.GetKeyDown("space"))
        {
            PlayerJump();

            //m_Animator.speed = 3;

            //playerAnimation.ResetTrigger("JumpTrigger");
        }

        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
            initialPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButton(0))
        {
            finalPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            if (isSwiping && CheckJumpSwipe())
            {
                PlayerJump();
                isSwiping = false;
            }
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
                transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
                isOnTheRightLane = true;

            }
        }

    }
    private bool CheckJumpSwipe()
    {
        Vector2 swipeVector = finalPosition - initialPosition;
        float swipeVectorMagnitude = swipeVector.magnitude;
        float swipeSin = swipeVector.y / swipeVectorMagnitude;

        Debug.Log(swipeSin);

        return swipeSin >= Mathf.Sin(Mathf.Deg2Rad * jumpSwipeDegree) &&
               swipeVectorMagnitude / screenHeight > swipeAccuracy;
    }

    private void PlayerJump()
    {
        playerRigidbody.useGravity = false;

        m_Animator.SetTrigger("jump");
    }
}
