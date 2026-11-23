using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private Transform spriteContainer;
    [SerializeField] private GameObject defaultPrefab;

    private List<GameObject> activeSprites = new List<GameObject>();

    private Coroutine transitionRoutine;

    public void ShowStep(List<SpriteConfig> configs)
    {
        StartTransition(configs, null);
    }

    public void ShowChoice(SpriteConfig config)
    {
        StartTransition(null, config);
    }

    private void StartTransition(
        List<SpriteConfig> configs,
        SpriteConfig choiceConfig)
    {
        if (transitionRoutine != null)
            StopCoroutine(transitionRoutine);

        transitionRoutine = StartCoroutine(
            ReplaceSprites(configs, choiceConfig)
        );
    }

    private IEnumerator ReplaceSprites(
        List<SpriteConfig> configs,
        SpriteConfig choiceConfig)
    {
        yield return ClearSpritesWithExit();

        if (configs != null)
        {
            foreach (SpriteConfig config in configs)
                SpawnSprite(config);
        }

        if (choiceConfig != null)
            SpawnSprite(choiceConfig);

        transitionRoutine = null;
    }

    private void SpawnSprite(SpriteConfig config)
    {
        GameObject prefabToUse =
            config.prefab != null ? config.prefab : defaultPrefab;

        Transform parent = spriteContainer != null
            ? spriteContainer
            : transform;

        GameObject instance = Instantiate(
            prefabToUse,
            new Vector3(config.spawnPos.x, config.spawnPos.y, 100f),
            Quaternion.identity,
            parent
        );

        SpriteRenderer spriteRenderer =
            instance.GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = config.sprite;

        SpriteAnimController animationController =
            instance.GetComponent<SpriteAnimController>();

        if (animationController == null)
            animationController =
                instance.AddComponent<SpriteAnimController>();

        animationController.Initialize(config.animations);

        activeSprites.Add(instance);
    }

    private IEnumerator ClearSpritesWithExit()
    {
        foreach (GameObject sprite in activeSprites)
        {
            if (sprite == null) continue;

            SpriteAnimController animationController = sprite.GetComponent<SpriteAnimController>();

            if (animationController != null)
                yield return StartCoroutine(animationController.PlayExit());

            Destroy(sprite);
        }

        activeSprites.Clear();
    }

    public void ClearAllSprites()
    {
        if (transitionRoutine != null)
        {
            StopCoroutine(transitionRoutine);
        }

        transitionRoutine = StartCoroutine(ClearRoutine());
    }

    private IEnumerator ClearRoutine()
    {
        yield return ClearSpritesWithExit();
        transitionRoutine = null;
    }
}