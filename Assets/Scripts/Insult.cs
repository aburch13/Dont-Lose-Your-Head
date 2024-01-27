using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Insult : MonoBehaviour
{
    [SerializeField]
    private string[] nobleAInsults;
    [SerializeField]
    private string[] nobleBInsults;
    [SerializeField]
    private string[] nobleCInsults;

    [SerializeField]
    private string[] nobleAPraise;
    [SerializeField]
    private string[] nobleBPraise;
    [SerializeField]
    private string[] nobleCPraise;

    [SerializeField]
    private string[] lawInsults;
    [SerializeField]
    private string[] jesterInsults;

    [SerializeField]
    private GameObject speechBubble;
    [SerializeField]
    private TextMeshProUGUI textBlock;
    [SerializeField]
    private float chatTime;


    public void Bark(int subject)
    {
        speechBubble.SetActive(true);
        switch(subject)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
        }

        chatTime = 4f;
    }

    private void Update()
    {
        if(chatTime > 0)
        {
            chatTime -= Time.deltaTime;
            if(chatTime < 0)
            {
                speechBubble.SetActive(false);
            }
        }
    }
}
