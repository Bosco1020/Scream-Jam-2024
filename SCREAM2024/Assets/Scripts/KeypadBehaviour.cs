using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.Events;

public class KeypadBehaviour : MonoBehaviour
{
    public Passcode generatedPasscode;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI interactPrompt;
    public GameObject keypadCanvas;
    public Color defaultColor = Color.black;
    public float interactionDistance = 5f;

    [SerializeField] private UnityEvent pause = new UnityEvent();
    [SerializeField] private UnityEvent unpause = new UnityEvent();

    public UnityEvent incorrectCode = new UnityEvent();
    public UnityEvent correctCode = new UnityEvent();

    private string generatedCode;
    private string userInput = "";
    private int maxDigits = 4;
    private GameObject player;
    private bool isNearKeypad = false;
    private bool keypadOpen = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        keypadCanvas.SetActive(false);
        interactPrompt.gameObject.SetActive(false);
        //generatedPasscode.clearCode();
        //generatedPasscode.appendCode(1234);
        StartCoroutine(GenerateCoroutine());
        ResetDisplay();
    }

    void Update()
    {
        
        if (isNearKeypad && Input.GetKeyDown(KeyCode.E))
        {
            if (keypadOpen)
            {
                DeactivateKeypad();
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                ActivateKeypad();
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (keypadOpen)
        {
            HandleKeypadInput();
        }
    }

    IEnumerator IncorrectCoroutine()
    {
        UpdateDisplay("INCORRECT");
        incorrectCode.Invoke(); 
        yield return new WaitForSeconds(2); 
        ResetDisplay();
    }

    IEnumerator CorrectCoroutine()
    {
        UpdateDisplay("CORRECT: EXIT OPEN"); 
        correctCode.Invoke();
        yield return new WaitForSeconds(2);
        DeactivateKeypad();
    }

    IEnumerator GenerateCoroutine()
    {
        yield return new WaitForSeconds(1);
        generatedCode = generatedPasscode.getCode();
        Debug.Log("Generated Code: " + generatedCode);

    }



    public void EntersRange()
    {
        isNearKeypad = true;
        interactPrompt.gameObject.SetActive(true);

    }

    public void ExitsRange()
    {
        isNearKeypad = false;
        interactPrompt.gameObject.SetActive(false);
        if (keypadOpen)
        {
            DeactivateKeypad();
        }
    }

    void HandleKeypadInput()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                OnNumberButtonClick(i.ToString());
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterButtonClick();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnClearButtonClick();
        }
    }

    public void OnNumberButtonClick(string number)
    {
        if (userInput.Length < maxDigits)
        {
            userInput += number;
            UpdateDisplay(userInput);
        }
    }

    void UpdateDisplay(string input)
    {
        displayText.text = input;
        displayText.color = defaultColor; // Reset color while inputting
    }

    public void OnEnterButtonClick()
    {
        if (userInput == generatedCode)
        {
            StartCoroutine(CorrectCoroutine());
        }
        else
        {
            StartCoroutine(IncorrectCoroutine());
        }
    }

    public void OnClearButtonClick()
    {
        userInput = "";
        ResetDisplay();
    }

    void ResetDisplay()
    {
        displayText.text = ""; // Clear the text
        userInput = "";
        displayText.color = defaultColor; // Reset to default color
    }

    public void DeactivateKeypad()
    {
        unpause.Invoke();
        keypadCanvas.SetActive(false);
        keypadOpen = false;
    }

    void ActivateKeypad()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pause.Invoke();
        keypadCanvas.SetActive(true);
        interactPrompt.gameObject.SetActive(false);
        keypadOpen = true;
    }
}
