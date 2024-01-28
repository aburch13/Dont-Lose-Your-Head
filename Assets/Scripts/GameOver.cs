using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private GameManager gameManager;


    public void MoveToGameOver(bool isFromNoble)
    {
        if(isFromNoble)
        {
            if(GameManager.week != 1)
            {
                if (GameManager.day != 0)
                    text.text = "A noble called for your head after " + GameManager.week + "weeks and " + (GameManager.day + 1) + "days!";
                else
                    text.text = "A noble called for your head after " + GameManager.week + "weeks and " + (GameManager.day + 1) + "day!";
            }
            else
            { if (GameManager.day != 0)
                    text.text = "A noble called for your head after " + GameManager.week + "week and " + (GameManager.day + 1) + "days!";
                else
                    text.text = "A noble called for your head after " + GameManager.week + "week and " + (GameManager.day + 1) + "day!";
            }
        }
        else
        {
            if (GameManager.week != 1)
            {
                text.text = "The Kingdom fell after " + GameManager.week + "weeks!";
            }
            else
            {
                text.text = "The Kingdom fell after only " + GameManager.week + "week!";
            }
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
