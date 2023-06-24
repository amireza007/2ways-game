using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArianWorkplace;

public class BallBehaviour : MonoBehaviour
{
    PlayerMovement playerMovementScript;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private SphereCollider playerCollider;
    [SerializeField] private SphereCollider ballCollider;
    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
    public void EndJumpAnimation()
    {
        rigidbody.useGravity = true;
    }
    public void StartJumpAnimation()
    {
        rigidbody.useGravity = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
            playerMovementScript.GameOverProcedure();
        }
    }
    public void TriggerCollider()
    {
        ballCollider.enabled = true;
        playerCollider.enabled = false;
    }
    public void EndTriggerCollider()
    {
        ballCollider.enabled = false;
        playerCollider.enabled = true;
    }
}
