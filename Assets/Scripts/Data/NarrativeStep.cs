using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeStep", menuName = "Narrative/new Step")]
public class NarrativeStep : ScriptableObject
{
    public string stepID;

    [TextArea(3, 5)]
    public string[] lines;
}
