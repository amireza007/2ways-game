using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ArianWorkplace;
public class sliderVal : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI Abovetext;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();

        slider.onValueChanged.AddListener((v) =>
        {
            Abovetext.text = v.ToString("0.00");
        });
    }

    // Update is called once per frame
    void Update()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            playerMovement.speed = v;
        });
    }
}
