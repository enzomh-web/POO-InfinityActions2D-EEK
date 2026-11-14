using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Transform spriteContainer;

    private List<GameObject> eventSprites = new List<GameObject>();
    private List<GameObject> stepSprites = new List<GameObject>();

    public void BeginEvent(NarrativeEvent narrativeEvent)
    {
        ClearAllSprites();

        foreach (SpriteConfig config in narrativeEvent.spriteConfig)
        {
            if (config.spawnTime == SpriteSpawnTime.DuringEvent)
            {
                SpawnSprite(config, eventSprites);
            }
        }
        
        EnterStep(narrativeEvent, 0);
    }

    public void EnterStep(NarrativeEvent narrativeEvent, int stepIndex)
    {
        ClearStepSprites();

        foreach (SpriteConfig config in narrativeEvent.spriteConfig)
        {
            if (config.spawnTime == SpriteSpawnTime.SpecificStep && config.stepIndex == stepIndex)
            {
                SpawnSprite(config, stepSprites);
            }
        }
    }

    private void SpawnSprite(SpriteConfig config, List<GameObject> spriteList)
    {
        Transform parent = spriteContainer;

        GameObject instance = Instantiate(config.prefab, new Vector3(config.spawnPos.x, config.spawnPos.y, 100f), Quaternion.identity, parent);

        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = config.sprite;
        spriteList.Add(instance);
    }

    private void ClearStepSprites()
    {
        foreach (GameObject sprite in stepSprites)
        {
            Destroy(sprite);
        }

        stepSprites.Clear();
    }

    public void ClearAllSprites()
    {
        ClearStepSprites();

        foreach (GameObject sprite in eventSprites)
        {
            Destroy(sprite);
        }

        eventSprites.Clear();
    }
}
