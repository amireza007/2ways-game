using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace ArianWorkplace
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float switchlaneSpeed;
        [SerializeField] private Transform rightLaneTransform;
        [SerializeField] private Transform leftLaneTransform;
        [SerializeField] private Animator animator;
        [Range(0, 3)] [SerializeField] private float squashSpeedMultiplier;

        private bool isOnRightSide;
        private bool isSwitchingLane = false;
        private Rigidbody rigidbody;


        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            transform.position = new Vector3(rightLaneTransform.position.x, transform.position.y, transform.position.z);
            isOnRightSide = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && !isSwitchingLane)
            {
                isOnRightSide = !isOnRightSide;

                StartCoroutine(SwitchingLaneWithLerp(isOnRightSide ? rightLaneTransform : leftLaneTransform));
            }
        }

        IEnumerator SwitchingLaneWithLerp(Transform laneTransform)
        {
            rigidbody.useGravity = false;
            isSwitchingLane = true;

            while (!Mathf.Approximately(transform.position.x, laneTransform.position.x))
            {
                Vector3 newPosition = new Vector3(laneTransform.position.x, transform.position.y,
                    transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPosition, switchlaneSpeed);

                yield return new WaitForEndOfFrame();
            }

            animator.SetFloat("SquashSpeedMultiplier", squashSpeedMultiplier);
            animator.SetTrigger("Squash");

            isSwitchingLane = false;
            rigidbody.useGravity = true;
        }

        private void FixedUpdate()
        {
            Vector3 newPosition = transform.position + Vector3.forward * (speed * Time.fixedTime);
            transform.position = newPosition;
        }
    }
}