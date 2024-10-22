using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenuUI;

    [SerializeField] private bool isPaused;

    [SerializeField] private UnityEvent pause = new UnityEvent();

    [SerializeField] private UnityEvent unpause = new UnityEvent();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            DeactivateMenu();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pause.Invoke();
        PauseMenuUI.SetActive(true);
        

    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        unpause.Invoke();
        PauseMenuUI.SetActive(false);
        isPaused = false;
        
    }


    public void EndGame()
    {   
        Application.Quit();
    }
}




