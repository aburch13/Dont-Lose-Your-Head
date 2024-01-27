using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noble : MonoBehaviour
{
    [SerializeField]
    private int lawOpinion;
    [SerializeField]
    private int jesterOpinion;
    [SerializeField]
    private Opinion opinion;
    [SerializeField]
    private GameObject ThoughtBubble;
    [SerializeField]
    private GameObject QuestionPanel;



    public void Mock(int intensity)
    {
        jesterOpinion -= intensity;
        jesterOpinion = Mathf.Clamp(jesterOpinion, -1, 4);
    }

    public void Praise(int intensity)
    {
        jesterOpinion += intensity;
        jesterOpinion = Mathf.Clamp(jesterOpinion, -1, 4);
    }

    public void LawPraised(int intensity)
    {
        lawOpinion += intensity;
        lawOpinion = Mathf.Clamp(lawOpinion, -2, 2);
        jesterOpinion -= Mathf.Abs(intensity);
        jesterOpinion = Mathf.Clamp(jesterOpinion, -1, 4);
    }
    public int GetLawOpinion() 
    { 
        return lawOpinion; 
    }
    public void SetLawOpinion(int intensity)
    {
        lawOpinion = intensity;
    }
    public void SetJesterOpinion(int intensity)
    {
        jesterOpinion = intensity;
    }
    public int GetJesterOpinion() 
    { 
        return jesterOpinion; 
    }
    public GameObject AskQuestion(int subject)
    {
        if(subject == 0)
        {
            opinion.DisplayOpinion(jesterOpinion, "Jester");
        }
        else if (lawOpinion > 0)
        {
            opinion.DisplayOpinion(2 + lawOpinion, "Right Law");
        }
        else if(lawOpinion < 0)
        {
            opinion.DisplayOpinion(2 + Mathf.Abs(lawOpinion), "Left Law");
        }
        else
        {
            opinion.DisplayOpinion(2 + lawOpinion, "Law");
        }
        ThoughtBubble.SetActive(true);
        QuestionPanel.SetActive(false);
        return ThoughtBubble;
    }
}
