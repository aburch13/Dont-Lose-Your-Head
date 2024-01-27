using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LawPanel : MonoBehaviour
{

    [SerializeField]
    private Image[] benefits = new Image[5];
    [SerializeField]
    private Image[] drawbacks = new Image[5];
    [SerializeField]
    private Sprite[] sprites = new Sprite[4];
    [SerializeField]
    private GameObject mockButton;


    public void DisplayLaw(Law law)
    {
        int hap = 0;
        int mon = 0;
        int foo = 0;
        int mil = 0;
        Law.Resource[] benefit = law.GetBenefit();
        Law.Resource[] hinder = law.GetHinder();
        for (int i = 0; i < benefit.Length; i++)
        {
            if (benefit[i] == Law.Resource.HAPPINESS) hap++;
            else if (benefit[i] == Law.Resource.MONEY) mon++;
            else if (benefit[i] == Law.Resource.FOOD) foo++;
            else if (benefit[i] == Law.Resource.MILITARY) mil++;
        }
        for (int i = 0; i < hinder.Length; i++)
        {
            if (hinder[i] == Law.Resource.HAPPINESS) hap--;
            else if (hinder[i] == Law.Resource.MONEY) mon--;
            else if (hinder[i] == Law.Resource.FOOD) foo--;
            else if (hinder[i] == Law.Resource.MILITARY) mil--;
        }

        for (int i = 0; i < benefits.Length; i++)
        {
            if (hap > 0)
            {
                hap--;
                benefits[i].sprite = sprites[0];
                benefits[i].enabled = true;
            }
            else if (mon > 0)
            {
                mon--;
                benefits[i].sprite = sprites[1];
                benefits[i].enabled = true;
            }
            else if (foo > 0)
            {
                foo--;
                benefits[i].sprite = sprites[2];
                benefits[i].enabled = true;
            }
            else if (mil > 0)
            {
                mil--;
                benefits[i].sprite = sprites[3];
                benefits[i].enabled = true;
            }
            else
            {
                benefits[i].enabled = false;
            }
        }
        for (int i = 0; i < drawbacks.Length; i++)
        {
            if (hap < 0)
            {
                hap++;
                drawbacks[i].sprite = sprites[0];
                drawbacks[i].enabled = true;
            }
            else if (mon < 0)
            {
                mon++;
                drawbacks[i].sprite = sprites[1];
                drawbacks[i].enabled = true;
            }
            else if (foo < 0)
            {
                foo++;
                drawbacks[i].sprite = sprites[2];
                drawbacks[i].enabled = true;
            }
            else if (mil < 0)
            {
                mil++;
                drawbacks[i].sprite = sprites[3];
                drawbacks[i].enabled = true;
            }
            else
            {
                drawbacks[i].enabled = false;
            }
        }
    }
    public void ToggleJoke(bool active)
    {
        mockButton.SetActive(active);
    }

    public void PassLaw(Law law)
    {
        gameObject.SetActive(true);
        DisplayLaw(law);

    }
}
