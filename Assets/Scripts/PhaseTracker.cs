using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseTracker : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void ChangePhase(GameManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameManager.GameState.QUESTION:
                text.text = "Inquire - Court is in session, choose a person to ask them their opinion on a subject! Or click a law to see its effects!";
                break;
            case GameManager.GameState.JOKE:
                text.text = "Entertain - It's time for your performance. Choose a subject for a joke and what kind of joke to make!";
                break;
            case GameManager.GameState.VOTE:

                break;
        }
    }
}
