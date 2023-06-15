using System;
using System.Collections;
using System.Collections.Generic;
using ArianWorkplace;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FogController fogController;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private UIManager uiManager;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fogController.enabled = true;
            playerMovement.enabled = true;
            
            uiManager.StartGame();
        }
    }
}
