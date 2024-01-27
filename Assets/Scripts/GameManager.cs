using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int Food;
    [SerializeField]
    private int Money;
    [SerializeField]
    private int Military;

    [SerializeField]
    private King king;
    [SerializeField]
    private Noble[] nobles = new Noble[3];

    public static int day;
    public static int week;


    [SerializeField]
    private Law lowLaw;
    [SerializeField]
    private Law highLaw;

    [SerializeField]
    private GameObject[] questionPanels;
    [SerializeField]
    private GameObject[] jokePanels;
    [SerializeField]
    private Button[] selectButtons;
    [SerializeField]
    private Slider[] resourceMeters;
    [SerializeField]
    private LawPanel lawPanel;
    [SerializeField]
    private Insult insult;
    [SerializeField]
    private GameObject gameOverScreen;

    private GameState gameState;
    private int currentTarget;
    private GameObject activeThoughtBubble;
    public enum GameState { QUESTION, JOKE, VOTE }
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.QUESTION;
        BuildLaws();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuildLaws()
    {
        if (week == 0)
        {
            highLaw.SetLaw(new Law.Resource[] { Law.Resource.HAPPINESS }, new Law.Resource[] { Law.Resource.FOOD, Law.Resource.FOOD, Law.Resource.FOOD });
            lowLaw.SetLaw(new Law.Resource[] { Law.Resource.FOOD, Law.Resource.FOOD, Law.Resource.FOOD }, new Law.Resource[] { Law.Resource.HAPPINESS});
            nobles[0].SetLawOpinion(2);
            nobles[1].SetLawOpinion(-2);
            nobles[2].SetLawOpinion(0);
        }

    }
    public void AskQuestion(int subjectID)
    {
        switch (currentTarget)
        {
            case 0: //King
                activeThoughtBubble = king.AskQuestion(subjectID);
                break;
            case 1: //Noble A
                activeThoughtBubble = nobles[0].AskQuestion(subjectID);

                break;
            case 2: //Noble B
                activeThoughtBubble = nobles[1].AskQuestion(subjectID);

                break;
            case 3: //Noble C
                activeThoughtBubble = nobles[2].AskQuestion(subjectID);

                break;
        }

        gameState = GameState.JOKE;
    }


    public void TellJoke(int subjectID)
    {
        switch (currentTarget)
        {
            case 1: //Noble A
                if(subjectID > 0)//Praise
                {
                    nobles[0].Praise(1);
                    nobles[1].Mock(1);
                    nobles[2].Mock(1);
                    king.NobleJested(1, 0);
                }
                else //insult
                {
                    nobles[0].Mock(1);
                    king.NobleJested(-1, 0);
                }
                break;
            case 2: //Noble B
                if (subjectID > 0)//Praise
                {
                    nobles[1].Praise(1);
                    nobles[0].Mock(1);
                    nobles[2].Mock(1);
                    king.NobleJested(1, 1);
                }
                else //insult
                {
                    nobles[1].Mock(1);
                    king.NobleJested(-1, 1);
                }
                break;
            case 3: //Noble C
                if (subjectID > 0)//Praise
                {
                    nobles[2].Praise(1);
                    nobles[0].Mock(1);
                    nobles[1].Mock(1);
                    king.NobleJested(1, 2);
                }
                else //insult
                {
                    nobles[2].Mock(1);
                    king.NobleJested(-1, 2);
                }
                break;
            case 4: //Jester
                nobles[0].Praise(1);
                nobles[1].Praise(1);
                nobles[2].Praise(1);

                break;
            case 5:
                nobles[0].LawPraised(1);
                nobles[1].LawPraised(1);
                nobles[2].LawPraised(1);
                break;
            case 6:
                nobles[0].LawPraised(-1);
                nobles[1].LawPraised(-1);
                nobles[2].LawPraised(-1);
                break;
        }
        activeThoughtBubble.SetActive(false);
        if (nobles[0].GetJesterOpinion() < 0 || nobles[1].GetJesterOpinion() < 0 || nobles[2].GetJesterOpinion() < 0)
        {
            GameOver();
            return;
        }
        day++;
        if (day == 5)
        {
            gameState = GameState.VOTE;
            Vote();
        }
        else
        {
            gameState = GameState.QUESTION;
        }
    }
    private void GameOver()
    {
        gameState = GameState.QUESTION;
        gameOverScreen.SetActive(true);
    }

    public void Select(int targetID)
    {
        
        switch (gameState)
        {
            case GameState.QUESTION:
                foreach (GameObject go in questionPanels)
                {
                    go.SetActive(false);
                }
                if (targetID == 5)
                {
                    lawPanel.DisplayLaw(highLaw);
                    lawPanel.ToggleJoke(false);
                }
                else if (targetID == 6)
                {
                    lawPanel.DisplayLaw(lowLaw);
                    lawPanel.ToggleJoke(false);
                }
                else
                {
                    questionPanels[targetID].SetActive(true);
                }
                break;
            case GameState.JOKE:
                foreach (GameObject go in jokePanels)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in questionPanels)
                {
                    go.SetActive(false);
                }
                if (targetID == 5)
                {
                    lawPanel.DisplayLaw(highLaw);
                    lawPanel.ToggleJoke(true);
                }
                else if (targetID == 6)
                {
                    lawPanel.DisplayLaw(lowLaw);
                    lawPanel.ToggleJoke(true);
                }
                else
                {
                    jokePanels[targetID].SetActive(true);
                }

                break;
        }
        currentTarget = targetID;
    }
    public void SetGameState(GameState state)
    {
        gameState = state;
    }

    private void Vote()
    {
        lawPanel.ToggleJoke(false);
        if (king.Vote(nobles[0].GetLawOpinion(), nobles[1].GetLawOpinion(), nobles[2].GetLawOpinion()))
        {
            highLaw.Pass();
            lawPanel.PassLaw(highLaw);
        }
        else
        {
            lowLaw.Pass();
            lawPanel.PassLaw(lowLaw);
        }
    }
    public void HappinessChange(int amount)
    {

    }

    public void MoneyChange(int amount)
    {
        resourceMeters[1].value += amount;
    }
    public void FoodChange(int amount)
    {
        resourceMeters[1].value += amount;
    }
    public void MilitaryChange(int amount)
    {
        resourceMeters[1].value += amount;
    }
}
