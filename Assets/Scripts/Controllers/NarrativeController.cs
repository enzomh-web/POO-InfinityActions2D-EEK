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
    [SerializeField] private NarrativeUI narrativeUI;
    private NarrativeEvent currentEvent;
    private int currentStepIndex;
    private int currentLineIndex;

    private bool isRunning;
    private bool awaitingChoice;

    public event System.Action OnNarrativeFinished;


    private void Update()
    {
        if (isRunning && !awaitingChoice && Input.anyKeyDown)
        {
            Advance();
        }
    }

    public void StarEvent(NarrativeEvent narrativeEvent)
    {
        narrativeUI.ClearChoices();

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
                narrativeUI.DisplayText(step.lines[currentLineIndex]);
                return;
            }

            currentStepIndex++;
            currentLineIndex = 0;
        }

        DisplayChoices();
    }

    private void DisplayChoices()
    {
        narrativeUI.ClearChoices();

        if (currentEvent.choices == null || currentEvent.choices.Length == 0)
        {
            FinishEvent();
            return;
        }

        awaitingChoice = true;

        narrativeUI.DisplayChoices(currentEvent.choices, SelectChoice);
    }

    private void SelectChoice(NarrativeChoice choice)
    {
        if (!awaitingChoice) return;

        awaitingChoice = false;
        narrativeUI.ClearChoices();

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

    private void FinishEvent()
    {
        isRunning = false;
        awaitingChoice = false;

        OnNarrativeFinished?.Invoke();
    }
}
