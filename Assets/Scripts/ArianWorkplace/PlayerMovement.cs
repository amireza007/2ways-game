using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace ArianWorkplace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [Range(0, 1)] [SerializeField] private float switchLaneSpeed;
        [SerializeField] private Transform rightLaneTransform;
        [SerializeField] private Transform rightLaneSquashTransform;
        [SerializeField] private Transform leftLaneTransform;
        [SerializeField] private Transform leftLaneSquashTransform;
        [SerializeField] private Animator ballAnimator;
        [Range(0, 3)] [SerializeField] private float squashAnimationSpeedMultiplier;
        [Range(0, 1)] [SerializeField] private float squashSpeed;

        private bool isOnRightSide;
        private bool isSwitchingLane = false;
        private Rigidbody playerRigidbody;


        private void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();

            transform.position = new Vector3(rightLaneTransform.position.x, transform.position.y, transform.position.z);
            isOnRightSide = true;
        }

        private void Update()
        {
            Vector3 newPosition = transform.position + Vector3.forward * (speed * Time.deltaTime);
            transform.position = newPosition;
            
            if (Input.GetKeyDown(KeyCode.T) && !isSwitchingLane)
            {
                isOnRightSide = !isOnRightSide;

                StartCoroutine(SwitchingLaneWithLerp(isOnRightSide ? rightLaneTransform : leftLaneTransform,
                    isOnRightSide ? rightLaneSquashTransform : leftLaneSquashTransform));
            }
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
                    ballAnimator.SetFloat("SquashSpeedMultiplier", squashAnimationSpeedMultiplier);
                    ballAnimator.SetTrigger("Squash");
                    
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

    }
}