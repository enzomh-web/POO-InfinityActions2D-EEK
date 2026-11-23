using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NarrativeStep
{
    [TextArea(3, 5)]
    public string text;
    public List<SpriteConfig> spriteConfigs;
}
