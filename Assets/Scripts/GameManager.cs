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


    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    instance = singleton.AddComponent<GameManager>();
                    singleton.name = "GameManagerSingleton";
                    DontDestroyOnLoad(singleton);
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator LevelFinished()
    {
        yield return new WaitForSeconds(0.75f);
        
        uiManager.LevelFinished();
    }

    public void StartLevel()
    {
        fogController.enabled = true;
        playerMovement.enabled = true;
    }
}