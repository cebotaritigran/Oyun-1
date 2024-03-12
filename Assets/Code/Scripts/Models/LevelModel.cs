using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int index;
    public int cardDeckWidth;
    public int cardDeckHeight;
    public int numberOfCards;
    public int pairAmount;
    public float offset;
    public List<Vector3> cardPositions = new List<Vector3>();

    public Level(int index, int cardDeckWidth, int cardDeckHeight)
    {
        this.index = index;
        this.cardDeckWidth = cardDeckWidth;
        this.cardDeckHeight = cardDeckHeight;

        this.numberOfCards = cardDeckWidth * cardDeckHeight;
        this.pairAmount = this.numberOfCards / 2;
        this.offset = 5.0f / this.numberOfCards;

        CalculateCardPositions();
    }

    private void CalculateCardPositions()
    {
        for (int x = 0; x < cardDeckWidth; x++)
        {
            for (int z = 0; z < cardDeckHeight; z++)
            {
                float cardWidth = 1.0f;
                float cardHeight = 1.45f;

                Vector3 position = new Vector3(x * (offset + cardWidth), 0.0f, z * (offset + cardHeight));
                cardPositions.Add(position);
            }
        }
    }
}