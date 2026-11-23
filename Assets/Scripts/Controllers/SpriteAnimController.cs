using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Runtime;
using UnityEngine.UIElements;

public class SpriteAnimController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private List<SpriteAnim> animations;

    public void Initialize(List<SpriteAnim> anims)
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animations = anims ?? new List<SpriteAnim>();

        StartCoroutine(PlayEntry());
    }

    private IEnumerator PlayEntry()
    {
        yield return StartCoroutine(PlayMoment(AnimationMoment.ENTRY));

        StartDuringAnimations();
    }

    private IEnumerator PlayMoment(AnimationMoment moment)
    {
        float longestDurantion = 0f;

        foreach (SpriteAnim anim in animations)
        {
            if (anim.animationMoment != moment || anim.animationType == AnimationType.NONE) continue;

            StartCoroutine(Animate(anim, moment));

            longestDurantion = Mathf.Max(longestDurantion, anim.duration);
        }

        yield return new WaitForSeconds(longestDurantion);
    }

    private void StartDuringAnimations()
    {
        foreach (SpriteAnim anim in animations)
        {
            if (anim.animationMoment != AnimationMoment.DURING || anim.animationType == AnimationType.NONE) continue;

            if (anim.animationType == AnimationType.ROTATE)
            {
                StartCoroutine(RotateContinuously(anim));
            }
            else
            {
                StartCoroutine(Animate(anim, AnimationMoment.DURING));
            }
        }
    }

    private IEnumerator Animate(SpriteAnim anim, AnimationMoment moment)
    {
        float duration = anim.duration;

        if (anim.animationType == AnimationType.FADE)
        {
            float startAlpha = spriteRenderer.color.a;
            float targetAlpha = moment == AnimationMoment.EXIT ? 0f : 1f;

            if (moment == AnimationMoment.ENTRY)
            {
                startAlpha = 0f;
                SetAlpha(0f);
            }

            if (duration <= 0f)
            {
                SetAlpha(targetAlpha);
                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / duration);
                SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, t));

                yield return null;
            }

            SetAlpha(targetAlpha);
        }

        else if (anim.animationType == AnimationType.MOVE)
        {
            Vector3 startPosition = transform.position;

            Vector3 targetPosition = new Vector3(
                anim.targetPos.x,
                anim.targetPos.y,
                transform.position.z
            );

            if (duration <= 0f)
            {
                transform.position = targetPosition;
                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / duration);

                transform.position = Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

                yield return null;
            }

            transform.position = targetPosition;
        }

        else if (anim.animationType == AnimationType.ROTATE)
        {
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = startRotation *
                Quaternion.Euler(0f, 0f, anim.rotationSpeed);

            if (duration <= 0f)
            {
                transform.rotation = targetRotation;
                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                float t = Mathf.Clamp01(elapsed / duration);

                transform.rotation = Quaternion.Slerp(
                    startRotation,
                    targetRotation,
                    t
                );

                yield return null;
            }

            transform.rotation = targetRotation;
        }
    }
    private IEnumerator RotateContinuously(SpriteAnim anim)
    {
        while (true)
        {
            transform.Rotate(
                0f,
                0f,
                anim.rotationSpeed * Time.deltaTime
            );

            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    public IEnumerator PlayExit()
    {
        StopAllCoroutines();

        yield return StartCoroutine(PlayMoment(AnimationMoment.EXIT));
    }
}
