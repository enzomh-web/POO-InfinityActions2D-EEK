using UnityEngine;

public enum AnimationType
{
    NONE,
    FADE,
    MOVE,
    ROTATE
}

public enum AnimationMoment
{
    ENTRY,
    DURING,
    EXIT    
}

[System.Serializable]
public class SpriteAnim
{
    public AnimationType animationType;
    public AnimationMoment animationMoment;

    [Header("Duration")]

    [Min(0f)]
    public float duration = 1f;

    [Header("Move")]
    public Vector2 targetPos;

    [Header("Rotate")]
    public float rotationSpeed = 90f;

}
