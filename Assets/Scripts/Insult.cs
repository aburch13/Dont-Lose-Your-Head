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
            case 0://Insult General
                textBlock.text = nobleAInsults[Random.Range(0, nobleAInsults.Length)];
                break;
            case 1://Insult Dowager
                textBlock.text = nobleBInsults[Random.Range(0, nobleBInsults.Length)];

                break;
            case 2://Insult Upstart

                textBlock.text = nobleCInsults[Random.Range(0, nobleCInsults.Length)];
                break;
            case 3://Praise General

                textBlock.text = nobleAPraise[Random.Range(0, nobleAPraise.Length)];
                break;
            case 4://Praise Dowager

                textBlock.text = nobleBPraise[Random.Range(0, nobleBPraise.Length)];
                break;
            case 5://Praise Upstart

                textBlock.text = nobleCPraise[Random.Range(0, nobleCPraise.Length)];
                break;
            case 6://Insult Law

                textBlock.text = lawInsults[Random.Range(0, lawInsults.Length)];
                break;
            case 7://Insult Jester
                textBlock.text = jesterInsults[Random.Range(0, jesterInsults.Length)];

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
