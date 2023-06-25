using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using UnityEngine.Serialization;

namespace ArianWorkplace
{
    enum Swipes
    {
        Up,
        Right,
        Left,
        NoSwipe
    }

    public class PlayerMovement : MonoBehaviour
    {
        AudioManager audioManager;
        [SerializeField] private float speed;
        [Range(0, 1)] [SerializeField] private float switchLaneSpeed;
        [SerializeField] private Transform rightLaneTransform;
        [SerializeField] private Transform rightLaneSquashTransform;
        [SerializeField] private Transform leftLaneTransform;
        [SerializeField] private Transform leftLaneSquashTransform;
        [SerializeField] private Animator ballParentAnimator;
        [SerializeField] private Animator ballAnimator;
        [Range(0, 3)] [SerializeField] private float squashAnimationSpeedMultiplier;
        [Range(0, 1)] [SerializeField] private float squashSpeed;

        [Range(0f, 1f)] [SerializeField] private float jumpSwipeAccuracy;
        [Range(0f, 1f)] [SerializeField] private float laneSwipeAccuracy;
        [SerializeField] private float jumpSwipeDegree;
        [SerializeField] private float decreaseRotateRate;

        [SerializeField] private FollowPlayer followPlayerScript;

        private float rotateRateMultiplier = 1;

        public float loadScreenSeconds = 2;
        public float torchPower = 4;

        private bool isOnRightSide;
        private bool isSwitchingLane = false;
        private Rigidbody playerRigidbody;
        private int screenHeight;
        private int screenWidth;

        private Vector2 initialPosition;
        private Vector2 finalPosition;
        private bool isSwiping;


        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            audioManager = FindAnyObjectByType<AudioManager>();
            audioManager.Play("MainTheme");
            screenHeight = Screen.height;
            screenWidth = Screen.width;

            transform.position = new Vector3(rightLaneTransform.position.x, transform.position.y, transform.position.z);
            isOnRightSide = true;
        }

        private void Update()
        {
            Vector3 newPosition = transform.position + Vector3.forward * (speed * Time.deltaTime);
            transform.position = newPosition;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJump();
            }

            if (Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                initialPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButton(0))
            {
                finalPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                if (isSwiping && CheckSwipe() != Swipes.NoSwipe)
                {
                    isSwiping = false;
                }
            }


            if (Input.GetKeyDown(KeyCode.T) && !isSwitchingLane)
            {
                isOnRightSide = !isOnRightSide;

                StartCoroutine(SwitchingLaneWithLerp(isOnRightSide ? rightLaneTransform : leftLaneTransform,
                    isOnRightSide ? rightLaneSquashTransform : leftLaneSquashTransform));
            }
        }

        private IEnumerator WaitForSceneLoad()
        {
            yield return new WaitForSeconds(loadScreenSeconds);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator SwitchingLaneWithLerp(Transform laneTransform, Transform laneSquashTransform)
        {
            //Switching lane
            bool isSquashTriggered = false;

            playerRigidbody.useGravity = false;
            isSwitchingLane = true;

            while (!Mathf.Approximately(transform.position.x, laneSquashTransform.position.x))
            {
                Vector3 newPosition = new Vector3(laneSquashTransform.position.x, transform.position.y,
                    transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, switchLaneSpeed);

                if (!isSquashTriggered && Mathf.Abs(transform.position.x) > Mathf.Abs(laneTransform.position.x))
                {
                    playerRigidbody.useGravity = true;

                    // Playing the squash animation

                    isSquashTriggered = true;
                    ballParentAnimator.SetFloat("SquashSpeedMultiplier", squashAnimationSpeedMultiplier);
                    ballParentAnimator.SetTrigger("Squash");
                }

                yield return new WaitForEndOfFrame();
            }


            while (!Mathf.Approximately(transform.position.x, laneTransform.position.x))
            {
                Vector3 newPosition = new Vector3(laneTransform.position.x, transform.position.y,
                    transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, squashSpeed);

                yield return new WaitForEndOfFrame();
            }

            isSwitchingLane = false;
        }

        IEnumerator StopRotating()
        {
            while (rotateRateMultiplier > 0)
            {
                rotateRateMultiplier -= decreaseRotateRate;
                ballAnimator.SetFloat("RotateRate", rotateRateMultiplier);

                yield return new WaitForEndOfFrame();
            }
        }

        private Swipes CheckSwipe()
        {
            Vector2 swipeVector = finalPosition - initialPosition;
            float swipeVectorMagnitude = swipeVector.magnitude;
            float swipeSin = swipeVector.y / swipeVectorMagnitude;
            float swipeCos = swipeVector.x / swipeVectorMagnitude;

            Debug.Log("Swipe Sin: " + swipeSin + "-- SwipeCos: " + swipeCos);
            Debug.Log(swipeVectorMagnitude);

            if ((swipeVectorMagnitude / screenHeight) > jumpSwipeAccuracy)
            {
                if (swipeSin > Mathf.Sin(Mathf.Deg2Rad * 45))
                {
                    PlayerJump();
                    return Swipes.Up;
                }
            }

            if (swipeVectorMagnitude / screenWidth > laneSwipeAccuracy)
            {
                if (!isSwitchingLane)
                {
                    if (swipeCos > Mathf.Cos(Mathf.Deg2Rad * 45))
                    {
                        if (!isOnRightSide)
                        {
                            StartCoroutine(SwitchingLaneWithLerp(rightLaneTransform, rightLaneSquashTransform));
                            isOnRightSide = !isOnRightSide;
                            return Swipes.Right;
                        }

                        return Swipes.NoSwipe;
                    }

                    if (swipeCos < -Mathf.Cos(Mathf.Deg2Rad * 45))
                    {
                        if (isOnRightSide)
                        {
                            StartCoroutine(SwitchingLaneWithLerp(leftLaneTransform, leftLaneSquashTransform));
                            isOnRightSide = !isOnRightSide;
                            return Swipes.Left;
                        }

                        return Swipes.NoSwipe;
                    }
                }
            }

            return Swipes.NoSwipe;
        }

        private void PlayerJump()
        {
            playerRigidbody.useGravity = false;

            ballParentAnimator.SetTrigger("Jump");
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("obstacle"))
            {
                GameOverProcedure();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Torch"))
            {
                Debug.Log(other.gameObject);
                audioManager.Play("TorchSound");
                DOTween.To(() => RenderSettings.fogStartDistance, x => RenderSettings.fogStartDistance = x,
                    RenderSettings.fogStartDistance + torchPower, 0.5f);

                DOTween.To(() => RenderSettings.fogEndDistance, x => RenderSettings.fogEndDistance = x,
                    RenderSettings.fogEndDistance + torchPower, 0.5f);

                Destroy(other.gameObject);
            }
            else if (other.CompareTag("Finish"))
            {
                LevelCompleted();
            }
            else if (other.CompareTag("Fall"))
            {
                GameOverProcedure();   
            }
        }

        private void LevelCompleted()
        {
            StartCoroutine(GameManager.Instance.LevelFinished());
        }

        public void GameOverProcedure()
        {
            foreach (var followPlayer in GameObject.FindObjectsByType<FollowPlayer>(FindObjectsSortMode.None))
            {
                followPlayer.enabled = false;
            }

            CameraShaker.Instance.ShakeOnce(0.4f, 4f, 0.5f, 2f);
            audioManager.Play("BallHit");
            StartCoroutine(audioManager.LowerVolume("MainTheme", 0.3f));

            this.enabled = false;

            StartCoroutine(StopRotating());

            playerRigidbody.AddForce(-80, 0, -50);

            StartCoroutine(GameManager.Instance.LevelFinished());
            //StartCoroutine(WaitForSceneLoad());
        }
    }
}