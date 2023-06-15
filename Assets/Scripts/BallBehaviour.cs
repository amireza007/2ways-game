using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    [SerializeField] private Rigidbody rigidbody;

    public void EndJumpAnimation()
    {
        rigidbody.useGravity = true;
    }
}
