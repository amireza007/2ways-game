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
    [SerializeField] private RoadManager roadManager;
    [SerializeField] public Animator ballAnimator;
    public static int highscore;
    public static int currentscore = 0;

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
    private void Start()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);

    }
    private void Awake()
    {
        ballAnimator.SetFloat("RotateRate",0);

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
        ballAnimator.speed = 2;
        ballAnimator.SetFloat("RotateRate", 1);
        Debug.Log(ballAnimator.speed);
        fogController.enabled = true;
        playerMovement.enabled = true;
    }
}