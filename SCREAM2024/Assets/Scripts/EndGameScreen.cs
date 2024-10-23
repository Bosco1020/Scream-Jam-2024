using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public string gameOverText;
    public string victoriousText;

    public void updateState(bool isVictory)
    {
        if(isVictory)
        {
            text.text = victoriousText;
        }
        else
        {
            text.text = gameOverText;
        }
    }
}
