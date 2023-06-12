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
    //public List<GameObject> torches = new List<GameObject>();

    public Rigidbody playerRigidbody;
    // Start is called before the first frame update

    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -20f, 0);
    }
    

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(m_Animator.speed);
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
        playerRigidbody.useGravity = true;
        Debug.Log("event triggered");
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
