using System;
using System.Net.Mime;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using ArianWorkplace;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private GameObject startUI;
    [SerializeField] private Button resetButton;
    [SerializeField] private RectTransform levels;
    [SerializeField] private Slider speedSlider;
    //[SerializeField] private TextMeshProUGUI speedSliderText
    public TMP_Text hscore;
    GameObject player;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private RectTransform finishUI;

    private Animator levelsSelectorAnimator;

    private void Awake()
    {
        backgroundButton.onClick.AddListener(StartGame);
        //levelNumberText.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1);
        levelNumberText.text = "ENDLESS\nVERSION";
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hscore.rectTransform.localPosition = new Vector3(400,0,0);
        hscore.text = "Current Score: 0\nHigh Score:" + GameManager.highscore;
        levelsSelectorAnimator = levels.GetComponent<Animator>();
    }
    private void Update()
    {
        hscore.text = "Current Score:" + GameManager.currentscore +"\nHigh Score:" + GameManager.highscore;

    }
    public void StartGame()
    {
        startUI.SetActive(false);
        //resetButton.GetComponent<TextMeshPro>().SetText("ok");
        resetButton.gameObject.SetActive(true);
        speedSlider.gameObject.SetActive(true);
        GameManager.Instance.StartLevel();
    }

    public void OpenLevelSelector()
    {
        levelsSelectorAnimator.SetTrigger("Open");

        backgroundButton.onClick.RemoveAllListeners();
        backgroundButton.onClick.AddListener(CloseLevelSelector);
    }

    private void CloseLevelSelector()
    {
        levelsSelectorAnimator.SetTrigger("Close");

        backgroundButton.onClick.RemoveAllListeners();
        backgroundButton.onClick.AddListener(StartGame);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SelectLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void LevelFinished()
    {
        resetButton.gameObject.SetActive(false);
        finishUI.gameObject.SetActive(true);
        
    }

    public void GoNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}