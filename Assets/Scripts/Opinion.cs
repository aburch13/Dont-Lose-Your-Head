using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Opinion : MonoBehaviour
{
    [SerializeField]
    private Sprite[] opinionIcons = new Sprite[5];
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI label;

    public void DisplayOpinion(int opinion, string name)
    {
        label.text = name;
        image.sprite = opinionIcons[opinion];
    }
}
