using System.Collections;
using System.Collections.Generic;
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
}
