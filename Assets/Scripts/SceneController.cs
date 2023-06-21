using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [SerializeField] private GameObject _losePanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenGameLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenLosePanel()
    {
        _losePanel.SetActive(true);
    }
}
