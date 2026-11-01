using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeChoice", menuName = "Narrative/new Choice")]
public class NarrativeChoice : ScriptableObject
{
    public string choiceID;
    public string text;
    public NarrativeOutcome[] outcomes;
}
