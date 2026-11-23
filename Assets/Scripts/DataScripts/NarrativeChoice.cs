using UnityEngine;

[System.Serializable]
public class NarrativeChoice
{
    public string text;
    [TextArea(3, 5)]
    public string responseText;
    public SpriteConfig spriteConfig;
    public NarrativeOutcome[] outcomes;
}
