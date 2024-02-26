using System.Collections;
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
        this.facedUp = faceUp;
        animator.SetBool("FaceUp", faceUp);
        sound.Play();
    }

    /*public int GetCardId()
    {
        return cardId;
    }*/
    public float durationSeconds = 2.5f;

    public void AnimateCardIntoPosition(Vector3 targetPosition)
    {
        //StopAllCoroutines();
        StartCoroutine(Animate(targetPosition));
    }

    private IEnumerator Animate(Vector3 targetPosition)
    {
        float timeElapsed = 0;

        // START ANIMATION FROM:
        // X COORDINATE FROM FINAL POSITION (NO ANIMATION)
        // Y COORDINATE FROM 5.0f
        // Z COORDINATE FROM THE ORIGINAL POSITION (PROBABLY 0.0f ???)
        Vector3 startPosition = new Vector3(targetPosition.x, 5.0f, transform.position.z);

        while (timeElapsed < durationSeconds)
        {
            //float t = EasingFunctions.EaseOutQuint(0, 1, timeElapsed / durationSeconds);
            float t = EasingFunctions.EaseOutExpo(0, 1, timeElapsed / durationSeconds);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
