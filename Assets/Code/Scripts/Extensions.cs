using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

static class MyExtensions
{
    public static IEnumerator AnimateToPosition(this Transform myObject, Vector3 startPosition, Vector3 targetPosition, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);
            myObject.position = Vector3.Lerp(startPosition, targetPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        myObject.position = targetPosition;
    }

    public static IEnumerator AnimateRotation(this Transform myObject, Vector3 startRotation, Vector3 targetRotation, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);
            myObject.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
            timeElapsed += Time.deltaTime;
            yield return 1;
        }
        myObject.eulerAngles = targetRotation;
    }

    public static IEnumerator AnimateScale(this Transform myObject, Vector3 startScale, Vector3 targetScale, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);
            myObject.localScale = Vector3.Lerp(startScale, targetScale, t);
            timeElapsed += Time.deltaTime;
            yield return 1;
        }
        myObject.localScale = targetScale;
    }
}