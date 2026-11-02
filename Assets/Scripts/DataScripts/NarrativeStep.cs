using UnityEngine;

[System.Serializable]
public class NarrativeStep
{
    public string stepID;

    [TextArea(3, 5)]
    public string[] lines;
}
