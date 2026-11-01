using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AI;
public class NarrativeController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private TMP_Text textBox;

    [Header("Escolhas (Choices)")]
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;


    private NarrativeEvent currentEvent;
    private int currentStepIndex;
    private int currentLineIndex;

    private bool isRunning;
    private bool awaitingChoice;


    private void Update()
    {
        if (isRunning && !awaitingChoice && Input.anyKeyDown)
        {
            Advance();
        }
    }

    public void StarEvent(NarrativeEvent narrativeEvent)
    {
        ClearChoices();

        currentEvent = narrativeEvent;
        currentStepIndex = 0;
        currentLineIndex = 0;

        isRunning = true;
        awaitingChoice = false;

        DisplayCurrentLine();
    } 

    private void Advance()
    {
        if (!isRunning) return;

        currentLineIndex++;

        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        while (currentStepIndex < currentEvent.steps.Length)
        {
            NarrativeStep step = currentEvent.steps[currentStepIndex];

            if (step != null && step.lines != null && currentLineIndex < step.lines.Length)
            {
                textBox.text = step.lines[currentLineIndex];
                return;
            }

            currentStepIndex++;
            currentLineIndex = 0;
        }

        DisplayChoices();
    }

    private void DisplayChoices()
    {
        ClearChoices();

        if (currentEvent.choices == null || currentEvent.choices.Length == 0)
        {
            FinishEvent();
            return;
        }

        awaitingChoice = true;

        foreach (NarrativeChoice choice in currentEvent.choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choicesContainer);

            Button button = buttonObj.GetComponent<Button>();
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();

            buttonText.text = choice.text;

            button.onClick.AddListener(() => SelectChoice(choice));
        }
    }

    private void SelectChoice(NarrativeChoice choice)
    {
        if (!awaitingChoice) return;

        awaitingChoice = false;
        ClearChoices();

        NarrativeEvent nextEvent =  GetRandomOutcome(choice);

        if (nextEvent == null)
        {
            FinishEvent();
            return;
        }

        StarEvent(nextEvent);
    }

    private NarrativeEvent GetRandomOutcome(NarrativeChoice choice)
    {
        float randNum = Random.Range(0f, 100f);
        float accProbability = 0f;

        foreach (NarrativeOutcome outcome in choice.outcomes)
        {
            accProbability += outcome.probability;
            
            if (randNum < accProbability)
            {
                return outcome.targetEvent;
            }
        }

        return choice.outcomes[choice.outcomes.Length - 1]. targetEvent;
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void FinishEvent()
    {
        isRunning = false;
        awaitingChoice = false;

        Debug.Log("NarrativeEvent finalizado!");
    }
}
