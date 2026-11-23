using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteConfig
{
    public Vector2 spawnPos;
    public GameObject prefab;
    public Sprite sprite;
    public List<SpriteAnim> animations;
}
