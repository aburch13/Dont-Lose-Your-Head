using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
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
    [SerializeField]
    private PhaseTracker phaseTracker;
    [SerializeField]
    private GameOver gameOver;
    [SerializeField]
    private TextMeshProUGUI dayCounter;

    private GameState gameState;
    private int currentTarget;
    private GameObject activeThoughtBubble;
    public enum GameState { QUESTION, JOKE, VOTE }
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.QUESTION;
        BuildLaws();
        phaseTracker.ChangePhase(gameState);
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
        else
        {
            highLaw.ShuffleLaw();
            lowLaw.ShuffleLaw();
            nobles[0].SetLawOpinion(Random.Range(-2, 3));
            nobles[1].SetLawOpinion(Random.Range(-2, 3));
            nobles[2].SetLawOpinion(Random.Range(-2, 3));

            king.NobleJested(Random.Range(-1, 2), 0);
            king.NobleJested(Random.Range(-1, 2), 1);
            king.NobleJested(Random.Range(-1, 2), 2);
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
        phaseTracker.ChangePhase(gameState);
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
                    insult.Bark(3);
                }
                else //insult
                {
                    nobles[0].Mock(1);
                    king.NobleJested(-1, 0);
                    insult.Bark(0);
                }
                break;
            case 2: //Noble B
                if (subjectID > 0)//Praise
                {
                    nobles[1].Praise(1);
                    nobles[0].Mock(1);
                    nobles[2].Mock(1);
                    king.NobleJested(1, 1);
                    insult.Bark(4);
                }
                else //insult
                {
                    nobles[1].Mock(1);
                    king.NobleJested(-1, 1);
                    insult.Bark(1);
                }
                break;
            case 3: //Noble C
                if (subjectID > 0)//Praise
                {
                    nobles[2].Praise(1);
                    nobles[0].Mock(1);
                    nobles[1].Mock(1);
                    king.NobleJested(1, 2);
                    insult.Bark(5);
                }
                else //insult
                {
                    nobles[2].Mock(1);
                    king.NobleJested(-1, 2);
                    insult.Bark(2);
                }
                break;
            case 4: //Jester
                nobles[0].Praise(1);
                nobles[1].Praise(1);
                nobles[2].Praise(1);

                insult.Bark(7);
                break;
            case 5:
                nobles[0].LawPraised(1);
                nobles[1].LawPraised(1);
                nobles[2].LawPraised(1);
                lawPanel.gameObject.SetActive(false);
                    insult.Bark(6);
                break;
            case 6:
                nobles[0].LawPraised(-1);
                nobles[1].LawPraised(-1);
                nobles[2].LawPraised(-1);
                lawPanel.gameObject.SetActive(false);
                    insult.Bark(6);
                break;
        }
        activeThoughtBubble.SetActive(false);
        if (nobles[0].GetJesterOpinion() < 0 || nobles[1].GetJesterOpinion() < 0 || nobles[2].GetJesterOpinion() < 0)
        {
            GameOver(true);
            return;
        }
        day++;
        dayCounter.text = "Week " + (week + 1) + "\n" + "Day " + (day + 1);
        if (day == 5)
        {
            gameState = GameState.VOTE;
            Vote();
        }
        else
        {
            gameState = GameState.QUESTION;
            phaseTracker.ChangePhase(gameState);
        }
    }
    private void GameOver(bool isFromNoble)
    {
        Debug.LogError("uhoh you died");
        gameState = GameState.QUESTION;
        gameOverScreen.SetActive(true);
        gameOver.MoveToGameOver(isFromNoble);
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
                if (targetID == 4)
                {
                    return;
                }
                else if (targetID == 5)
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
        if(Food <= 0 || Money <= 0 || Military <= 0)
        {
            GameOver(false);
        }
        else
        {
            week++;
            day = 0;
            dayCounter.text = "Week " + (week + 1) + "\n" + "Day " + (day + 1);
            gameState = GameState.QUESTION;
            phaseTracker.ChangePhase(gameState);
            BuildLaws();
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

    public void ResetGame()
    {
        week = 0;
        day = 0;
        dayCounter.text = "Week " + (week + 1) + "\n" + "Day " + (day + 1);
        gameState = GameState.QUESTION;
        BuildLaws();
        
    }
}
