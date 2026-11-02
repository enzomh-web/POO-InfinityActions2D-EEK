using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
public class NarrativeUI : MonoBehaviour
{
    [Header("Dialogo")]
    [SerializeField] private TMP_Text dialogueText;

    [Header("Escolhas")]
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    public void DisplayText(string text)
    {
        dialogueText.text = text;
    }

    public void DisplayChoices(NarrativeChoice[] choices, Action<NarrativeChoice> onChoiceSelected)
    {
        ClearChoices();


        foreach (NarrativeChoice choice in choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choicesContainer);

            Button button = buttonObj.GetComponent<Button>();
            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();

            buttonText.text = choice.text;

            button.onClick.AddListener(() => onChoiceSelected?.Invoke(choice));
        }
    }

    public void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
