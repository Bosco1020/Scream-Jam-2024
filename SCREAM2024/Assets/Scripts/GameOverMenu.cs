using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameOverMenuUI;

    [SerializeField] private bool isOver;

    [SerializeField] private UnityEvent pause = new UnityEvent();

    [SerializeField] private UnityEvent OnRestart = new UnityEvent();

    void Start()
    {
        GameOverMenuUI.SetActive(false);
    }

    void Update()
    {
        if (isOver)
        {
            ActivateEndGameMenu();
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ActivateEndGameMenu()
    {
        
        Time.timeScale = 0; 
        AudioListener.pause = true;
        //Debug.Log("TRYING TO PAUSE THIS");
        pause.Invoke();
        GameOverMenuUI.SetActive(true);
        isOver = true;
    }

    public void EndGame()
    {
        Application.Quit(); 
    }

    public void RestartGame()
    {
        isOver = false;
        Time.timeScale = 1;
        AudioListener.pause = false; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void TriggerGameOver()
    {
        isOver = true; // Set the game state to over
    }
}