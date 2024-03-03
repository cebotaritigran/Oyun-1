using UnityEngine;

public class Card : MonoBehaviour
{
    public AudioSource sound;
    public int cardId;
    public SpriteRenderer spriteRendererCardFront;
    public Animator animator;
    public bool facedUp = false;

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

    /*public int GetCardId()
    {
        return cardId;
    }*/

    public void AnimateCardIntoPosition(Vector3 targetPosition)
    {
        //StopAllCoroutines();

        // START ANIMATION FROM:
        // X COORDINATE FROM FINAL POSITION (NO ANIMATION)
        // Y COORDINATE FROM 5.0f
        // Z COORDINATE FROM THE ORIGINAL POSITION (PROBABLY 0.0f ???)
        Vector3 startPosition = new Vector3(targetPosition.x, 5.0f, transform.position.z);
        float durationSeconds = 2.5f;
        StartCoroutine(transform.AnimateToPosition(startPosition, targetPosition, durationSeconds, EasingFunctions.EaseOutExpo));
    }

    public void AnimateCardRotation()
    {
        //StopAllCoroutines();
        Vector3 startRotation = new Vector3(180.0f, transform.eulerAngles.y, transform.eulerAngles.z);
        Vector3 targetRotation = new Vector3(360.0f, 0.0f, 0.0f);
        float durationSeconds = 2.5f;
        StartCoroutine(transform.AnimateRotation(startRotation, targetRotation, durationSeconds, EasingFunctions.EaseOutExpo));
    }
}
