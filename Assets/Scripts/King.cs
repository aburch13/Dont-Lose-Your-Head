using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    [SerializeField]
    private int NobleAOpinion;
    [SerializeField]
    private int NobleBOpinion;
    [SerializeField]
    private int NobleCOpinion;
    [SerializeField]
    private Opinion opinion;
    [SerializeField]
    private GameObject ThoughtBubble;

    public void NobleJested(int intensity, int nobleID)
    {
        switch(nobleID)
        {
            case 0:
                NobleAOpinion += intensity;
                NobleAOpinion = Mathf.Clamp(NobleAOpinion, 0, 4);
                break;
            case 1:
                NobleBOpinion += intensity;
                NobleBOpinion = Mathf.Clamp(NobleBOpinion, 0, 4);
                break;
            case 2:
                NobleCOpinion += intensity;
                NobleCOpinion = Mathf.Clamp(NobleCOpinion, 0, 4);
                break;
        }
    }

    public bool Vote(int NobleA, int NobleB, int NobleC)
    {
        int tally = 0;
        tally += NobleA * NobleAOpinion;
        tally += NobleB * NobleBOpinion; 
        tally += NobleC * NobleCOpinion; 
        if(tally == 0)
        {
            return Random.Range(0, 2) > 0;
        }

        return tally < 0;
    }

    public GameObject AskQuestion(int nobleID)
    {
        switch (nobleID)
        {
            case 0:
                opinion.DisplayOpinion(NobleAOpinion, "Noble A");

                break;
            case 1:
                opinion.DisplayOpinion(NobleBOpinion, "Noble B");

                break;
            case 2:
                opinion.DisplayOpinion(NobleCOpinion, "Noble C");

                break;
        }
        ThoughtBubble.SetActive(true);
        return ThoughtBubble;
    }
}
