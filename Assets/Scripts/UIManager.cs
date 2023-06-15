using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text startText;
    [SerializeField] private Button resetButton;
    
    public void StartGame()
    {
        startText.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(true);
    }
    
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
