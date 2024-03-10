using System;
using System.Collections;
using UnityEngine;

static class MyExtensions
{
    // THANKS TO CHATGPT :))
    public static IEnumerator AnimateToPosition(this Transform myObject, Vector3 targetPosition, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        Vector3 totalDisplacement = targetPosition - myObject.position; // Total displacement needed
        Vector3 previousCumulativeDisplacement = Vector3.zero; // To store the displacement up to the previous frame

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);

            Vector3 currentCumulativeDisplacement = Vector3.Lerp(Vector3.zero, totalDisplacement, t); // Cumulative displacement up to the current frame
            Vector3 displacementThisFrame = currentCumulativeDisplacement - previousCumulativeDisplacement; // Calculate the displacement for this frame
            myObject.position += displacementThisFrame; // Add the displacement for this frame to the current position
            previousCumulativeDisplacement = currentCumulativeDisplacement; // Update the previous cumulative displacement
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Vector3 finalDisplacement = totalDisplacement - previousCumulativeDisplacement;
        myObject.position += finalDisplacement;
    }

    public static IEnumerator AnimateToPositionXAxis(this Transform myObject, float targetXCoordinate, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        Vector3 targetPosition = new Vector3(targetXCoordinate, myObject.position.y, myObject.position.z);

        return AnimateToPosition(myObject, targetPosition, durationSeconds, easing, delaySeconds);
    }

    public static IEnumerator AnimateToPositionYAxis(this Transform myObject, float targetYCoordinate, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        Vector3 targetPosition = new Vector3(myObject.position.x, targetYCoordinate, myObject.position.z);

        return AnimateToPosition(myObject, targetPosition, durationSeconds, easing, delaySeconds);
    }

    public static IEnumerator AnimateToPositionZAxis(this Transform myObject, float targetZCoordinate, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        Vector3 targetPosition = new Vector3(myObject.position.x, myObject.position.y, targetZCoordinate);

        return AnimateToPosition(myObject, targetPosition, durationSeconds, easing, delaySeconds);
    }

    public static IEnumerator AnimatePositionFromTo(this Transform myObject, Vector3 startPosition, Vector3 targetPosition, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
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

    public static IEnumerator AnimateEulerAngles(this Transform myObject, Vector3 startRotation, Vector3 targetRotation, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);
            myObject.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        myObject.eulerAngles = targetRotation;
    }

    public static IEnumerator AnimateRotation(this Transform myObject, Quaternion startRotation, Quaternion targetRotation, float durationSeconds, Func<float, float, float, float> easing, float delaySeconds = 0.0f)
    {
        yield return new WaitForSeconds(delaySeconds);
        float timeElapsed = 0;

        while (timeElapsed < durationSeconds)
        {
            float t = easing(0, 1, timeElapsed / durationSeconds);
            myObject.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        myObject.rotation = targetRotation;
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
            yield return null;
        }
        myObject.localScale = targetScale;
    }
}