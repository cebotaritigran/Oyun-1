using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour instance;
    private int cardDeckWidth = CardManager.width;
    private int cardDeckHeight = CardManager.height;
    private float offset = CardManager.offset;
    private float xCoordinate;
    private float zCoordinate;

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;
        // SET CAMERA POSITION TO THE CENTER OF ALL CARDS IN THE X AXIS
        xCoordinate = CalculateCameraXCoordinate();
        zCoordinate = CalculateCameraZCoordinate();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCameraPosition();
        AnimateCameraIntoPosition();
    }

    private void SetCameraPosition()
    {
        transform.position = new Vector3(xCoordinate, 4.5f, -4.5f);
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE X AXIS
    private float CalculateCameraXCoordinate()
    {
        //float cardWidth = 1.0f;

        int a = cardDeckWidth / 2;
        float xCoordinate = a * offset;
        if (cardDeckWidth % 2 == 0)
        {
            xCoordinate -= offset / 2;
        }

        return xCoordinate;
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE Z AXIS
    private float CalculateCameraZCoordinate()
    {
        //float cardHeight = 1.45f;

        int b = cardDeckHeight / 2;
        float zCoordinate = b * offset;
        if (cardDeckHeight % 2 == 0)
        {
            zCoordinate -= offset / 2;
        }
        return zCoordinate;
    }

    // SHOULD CALCULATE HOW FAR THE CAMERA SHOULD BE, IN ORDER TO SEE ALL THE CARDS
    private float CalculateCameraYCoordinate()
    {
        // CURRENT Y COORDINATE OF THE CAMERA
        float yCoordinate = transform.position.y;
        float maxOffscreen = 0.0f;

        for (int x = 0; x < cardDeckWidth; x++)
        {
            for (int z = 0; z < cardDeckHeight; z++)
            {
                Vector3 position = new Vector3(x * offset, 0, z * offset);

                // OBJECT'S COORDINATES ON THE SCREEN (NOT IN THE WORLD)
                // WHICH SHOULD ALWAYS BE BETWEEN 0 - 1
                // IF IT'S LESS THAN 0 OR GREATER 1, IT'S NOT ENTIRELY VISIBLE ON THE SCREEN
                // (BOTH X AND Y COORDINATES, Z DOESN'T MATTER BECAUSE THE SCREEN IS 2D)
                Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);

                // HERE I CHECK IT TO BE BETWEEN 0.1 - 0.9, TO LEAVE A LITTLE BIT OF SPACE AT THE EDGES OF THE SCREEN
                List<float> distances = new List<float>(){
                        (0.1f - screenPoint.x),
                        (0.1f - screenPoint.y),
                        (screenPoint.x - 0.9f),
                        (screenPoint.y - 0.9f),
                    };

                // AT THE END OF THESE NESTED FOR LOOPS, 
                // maxOffscreen WILL BE THE DISTANCE OF THE FURTHEST CARD
                // (THE CARD THAT'S THE FURTHEST FROM THE VIEWPORT)
                float maxDistance = distances.Max(e => e);
                if (maxDistance > maxOffscreen)
                {
                    //Debug.Log(maxDistance);
                    maxOffscreen = maxDistance;
                }
            }
        }

        return yCoordinate + (maxOffscreen * 10);
    }

    public async void AnimateCameraIntoPosition()
    {
        await Task.Delay(cardDeckWidth * cardDeckHeight * 50);
        float durationSeconds = 1.5f;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y, zCoordinate);

        StartCoroutine(transform.AnimateRotation(transform.eulerAngles, new Vector3(90.0f, 0.0f, 0.0f), durationSeconds, EasingFunctions.EaseInOutSine));
        StartCoroutine(transform.AnimateToPosition(currentPosition, targetPosition, durationSeconds, EasingFunctions.EaseInOutSine));

        //
        StartCoroutine(AnimateCameraHeight());
    }

    IEnumerator AnimateCameraHeight()
    {
        yield return new WaitForSeconds(1.5f);
        Vector3 cPosition = new Vector3(transform.position.x, CalculateCameraYCoordinate(), transform.position.z);
        StartCoroutine(transform.AnimateToPosition(transform.position, cPosition, 1.5f, EasingFunctions.EaseInOutSine));
    }
}
