using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public AudioSource sound;
    int _cardId;
    public SpriteRenderer spriteRendererCardFront;
    public Animator animator;

    public void SetCard(int id, Sprite sprite)
    {
        _cardId = id;
        spriteRendererCardFront.sprite = sprite;
    }
    public void FlipUp(bool faceUp)
    {
        animator.SetBool("FaceUp", faceUp);
        sound.Play();
    }

}
