using UnityEngine;

[System.Serializable]
public class NarrativeOutcome
{
    public NarrativeEvent targetEvent;

    [Range(0f, 100f)]
    public float probability;
}
