using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArianWorkplace;

public class BallBehaviour : MonoBehaviour
{
    PlayerMovement playerMovementScript;

    [SerializeField] private Rigidbody rigidbody;
    private void Start()
    {
        GetComponent<Collider>().enabled = false;
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
    public void EndJumpAnimation()
    {
        rigidbody.useGravity = true;
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
        GetComponent<SphereCollider>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SphereCollider>().enabled = false;
    }
    public void EndTriggerCollider()
    {
        GetComponent<SphereCollider>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SphereCollider>().enabled = true;
    }
}
