using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    public AudioSource sound;
    public int cardId;
    public SpriteRenderer spriteRendererCardFront;
    public Animator animator;
    public bool facedUp = false;
    private float durationSeconds = 2.5f;

    public void SetCard(int id, Sprite sprite)
    {
        cardId = id;
        spriteRendererCardFront.sprite = sprite;
    }
    public void FlipUp(bool faceUp)
    {
        facedUp = faceUp;
        animator.SetBool("FaceUp", faceUp);
        sound.Play();
    }

    public IEnumerator AnimateCardIntoPosition(Vector3 targetPosition)
    {
        // X COORDINATE STARTS FROM FINAL POSITION (NO ANIMATION)
        // Y COORDINATE STARTS FROM 5.0f
        // Z COORDINATE STARTS FROM THE ORIGINAL POSITION (PROBABLY 0.0f)
        Vector3 startPosition = new Vector3(targetPosition.x, 5.0f, transform.position.z);
        // RETURNS ONCE THE ANIMATION FINISHES
        yield return StartCoroutine(transform.AnimateToPosition(startPosition, targetPosition, durationSeconds, EasingFunctions.EaseOutExpo));
    }

    public IEnumerator AnimateCardRotation()
    {
        Vector3 startRotation = new Vector3(180.0f, transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 targetRotation = new Vector3(360.0f, 0.0f, 0.0f);
        // RETURNS ONCE THE ANIMATION FINISHES
        yield return StartCoroutine(transform.AnimateEulerAngles(startRotation, targetRotation, durationSeconds, EasingFunctions.EaseOutExpo));
    }
}
