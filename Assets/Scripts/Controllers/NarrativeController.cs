using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AI;
using System.Linq;
public class NarrativeController : MonoBehaviour
{
    [SerializeField] private NarrativeUI narrativeUI;
    [SerializeField] private NarrativeEvent centralEvent;
    [SerializeField] private SpriteController spriteController;

    private NarrativeEvent currentEvent;
    private NarrativeEvent nextEvent;

    private int currentStepIndex;

    private bool isRunning;
    private bool awaitingChoice;
    private bool awaitingOutcome;

    public event System.Action OnNarrativeFinished;


    private void Update()
    {
        if (isRunning && !awaitingChoice && Input.anyKeyDown)
        {
            Advance();
        }
    }

    public void StartEvent(NarrativeEvent narrativeEvent)
    {
        narrativeUI.ClearChoices();

        currentEvent = narrativeEvent;
        currentStepIndex = 0;

        isRunning = true;
        awaitingChoice = false;
        awaitingOutcome = false;

        spriteController.ClearAllSprites();

        DisplayCurrentText();
    } 

    private void Advance()
    {
        if (awaitingOutcome)
        {
            awaitingOutcome = false;

            if (nextEvent == null)
            {
                FinishEvent();
                return;
            }
        
            StartEvent(nextEvent);
            nextEvent = null;
            return;
        }

        if (!isRunning) return;
        
        currentStepIndex++;
        DisplayCurrentText();
    }

    private void DisplayCurrentText()
    {
        if (currentEvent.steps == null || currentStepIndex >= currentEvent.steps.Length)
        {
            DisplayChoices();
            return;
        }

        NarrativeStep currentStep = currentEvent.steps[currentStepIndex];

        if (currentStep == null)
        {
            Advance();
            return;
        }

        spriteController.ShowStep(currentStep.spriteConfigs);

        if (string.IsNullOrEmpty(currentStep.text))
        {
            Advance();
            return;
        }

        narrativeUI.DisplayText(currentStep.text);
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

        nextEvent = GetRandomOutcome(choice);

        if (!string.IsNullOrEmpty(choice.responseText))
        {
            spriteController.ShowChoice(choice.spriteConfig);

            narrativeUI.DisplayText(choice.responseText);
            awaitingOutcome = true;
        }
        else
        {
            StartEvent(nextEvent);
        }
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
        spriteController.ClearAllSprites();

        if (!currentEvent.isTerminalEvent)
        {
            currentEvent = centralEvent;
            StartEvent(currentEvent);
            return;
        }

        isRunning = false;
        awaitingChoice = false;

        OnNarrativeFinished?.Invoke();
    }
}
