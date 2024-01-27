using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Law : MonoBehaviour
{
    [SerializeField]
    private Resource[] benefit;
    [SerializeField]
    private Resource[] hinder;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public enum Resource
    { HAPPINESS, FOOD, MONEY, MILITARY }

    public void ShuffleLaw()
    {
        int benefitCount = Random.Range(1, 4);
        int hinderCount = benefitCount + Random.Range(-1, 2);

        benefit = new Resource[benefitCount];
        hinder = new Resource[hinderCount];

        bool[] ResourceID = new bool[4];

        for (int i = 0; i < benefitCount; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    benefit[i] = Resource.HAPPINESS;
                    ResourceID[0] = true;
                    break;
                case 1:
                    benefit[i] = Resource.FOOD;
                    ResourceID[1] = true;
                    break;
                case 2:
                    benefit[i] = Resource.MONEY;
                    ResourceID[2] = true;
                    break;
                case 3:
                    benefit[i] = Resource.MILITARY;
                    ResourceID[3] = true;
                    break;
            }
        }
        for (int i = 0; i < hinderCount; i++)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    if (ResourceID[0])
                    {
                        i--;
                        break;
                    }
                    hinder[i] = Resource.HAPPINESS;
                    break;
                case 1:
                    if (ResourceID[1])
                    {
                        i--;
                        break;
                    }
                    hinder[i] = Resource.FOOD;
                    break;
                case 2:
                    if (ResourceID[2])
                    {
                        i--;
                        break;
                    }
                    hinder[i] = Resource.MONEY;
                    break;
                case 3:
                    if (ResourceID[3])
                    {
                        i--;
                        break;
                    }
                    hinder[i] = Resource.MILITARY;
                    break;
            }
        }
    }
    public void SetLaw(Resource[] benefit, Resource[] hinder)
    {
        this.benefit = benefit;
        this.hinder = hinder;
    }

    public void Pass()
    {
        int hap = 0;
        int mon = 0;
        int foo = 0;
        int mil = 0;
        for (int i = 0;i < benefit.Length; i++)
        {
            if (benefit[i] == Resource.HAPPINESS) hap++;
            else if (benefit[i] == Resource.MONEY) mon++;
            else if (benefit[i] == Resource.FOOD) foo++;
            else if (benefit[i] == Resource.MILITARY) mil++;
        }
        for (int i = 0; i < hinder.Length; i++)
        {
            if (hinder[i] == Resource.HAPPINESS) hap--;
            else if (hinder[i] == Resource.MONEY) mon--;
            else if (hinder[i] == Resource.FOOD) foo--;
            else if (hinder[i] == Resource.MILITARY) mil--;
        }
        gameManager.HappinessChange(hap);
        gameManager.MoneyChange(mon);
        gameManager.FoodChange(foo);
        gameManager.MilitaryChange(mil);
    }
    
    public Resource[] GetBenefit()
    {
        return benefit;
    }
    public Resource[] GetHinder() 
    {
        return hinder; 
    }
}
