using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "NarrativeEvent", menuName = "Narrative/new Event")]
public class NarrativeEvent : ScriptableObject
{
    public string eventID;
    public NarrativeStep[] steps;
    public NarrativeChoice[] choices;
    public bool isTerminalEvent;
}
