using UnityEngine;

public enum SpriteSpawnTime
{
    DuringEvent,
    SpecificStep
}

[System.Serializable]
public class SpriteConfig
{
    public Vector2 spawnPos;
    public SpriteSpawnTime spawnTime;
    public int stepIndex;
    public GameObject prefab;
    public Sprite sprite;
}
