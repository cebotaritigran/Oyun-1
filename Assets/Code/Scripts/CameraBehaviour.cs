using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public static CameraBehaviour instance;
    private int cardDeckWidth;
    private int cardDeckHeight;
    private float offset;
    private float xCoordinate;
    private float zCoordinate;
    private float yCoordinate;
    private Vector3 finalCameraRotation = new Vector3(90.0f, 0.0f, 0.0f);
    private Camera fakeCamera;

    // Awake is used to initialize any variables or game state before the game starts
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // VARIABLES THAT DEPEND ON OTHER SCRIPTS SHOULD BE INITIALIZED IN `Start` METHOD
        // TO BE CERTAIN THAT THEY ARE ALREADY INITIALIZED IN THAT SCRIPT'S `Awake` METHOD
        cardDeckWidth = CardManager.width;
        cardDeckHeight = CardManager.height;
        offset = CardManager.offset;

        xCoordinate = CalculateCameraXCoordinate();
        zCoordinate = CalculateCameraZCoordinate();

        // CENTER THE CAMERA IN X AXIS
        transform.position = new Vector3(xCoordinate, 4.5f, -4.5f);
        InitializeFakeCamera();
        // `CalculateCameraYCoordinate` USES `fakeCamera` TO CALCULATE THE Y COORDINATE
        yCoordinate = CalculateCameraYCoordinate();
        AnimateCameraIntoPosition();
    }

    private void InitializeFakeCamera()
    {
        fakeCamera = GameObject.FindGameObjectWithTag("FakeCamera").GetComponent<Camera>();
        fakeCamera.transform.position = new Vector3(xCoordinate, transform.position.y, zCoordinate);
        fakeCamera.transform.eulerAngles = finalCameraRotation;
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE X AXIS
    private float CalculateCameraXCoordinate()
    {
        float cardWidth = 1.0f;

        float xCoordinate = (cardDeckWidth - 1) * (offset + cardWidth);
        xCoordinate /= 2;

        return xCoordinate;
    }

    // SHOULD CALCULATE THE CENTER OF THE CARD DECK, IN THE Z AXIS
    private float CalculateCameraZCoordinate()
    {
        float cardHeight = 1.45f;

        float zCoordinate = (cardDeckHeight - 1) * (offset + cardHeight);
        zCoordinate /= 2;

        return zCoordinate;
    }

    // SHOULD CALCULATE HOW FAR THE CAMERA SHOULD BE, IN ORDER TO SEE ALL THE CARDS
    private float CalculateCameraYCoordinate()
    {
        // CURRENT Y COORDINATE OF THE CAMERA
        float yCoordinate = transform.position.y;
        float maxOffscreen = 0.0f;

        for (int i = 0; i < CardManager.cardPositions.Count; i++)
        {
            Vector3 position = new Vector3(CardManager.cardPositions[i].x + 0.5f, 0, CardManager.cardPositions[i].z + 0.725f);

            // OBJECT'S COORDINATES ON THE SCREEN (NOT IN THE WORLD)
            // WHICH SHOULD ALWAYS BE BETWEEN 0 - 1
            // IF IT'S LESS THAN 0 OR GREATER 1, IT'S NOT ENTIRELY VISIBLE ON THE SCREEN
            // (BOTH X AND Y COORDINATES, Z DOESN'T MATTER BECAUSE THE SCREEN IS 2D)

            Vector3 screenPoint = fakeCamera.WorldToViewportPoint(position);

            // HERE I CHECK IT TO BE BETWEEN 0.15 - 0.85, TO LEAVE A LITTLE BIT OF SPACE AT THE EDGES OF THE SCREEN
            List<float> distances = new List<float>(){
                        (0.15f - screenPoint.x),
                        (0.15f - screenPoint.y),
                        (screenPoint.x - 0.85f),
                        (screenPoint.y - 0.85f),
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

        return yCoordinate + (maxOffscreen * 10);
    }

    public async void AnimateCameraIntoPosition()
    {
        // THIS IS AN ARBITRARY DURATION I CHOSE
        await Task.Delay(cardDeckWidth * cardDeckHeight * 50);
        float durationSeconds = 2.5f;

        Vector3 targetPosition = new Vector3(xCoordinate, yCoordinate, zCoordinate);

        StartCoroutine(transform.AnimateEulerAngles(transform.eulerAngles, finalCameraRotation, durationSeconds, EasingFunctions.EaseInOutSine));
        StartCoroutine(transform.AnimateToPosition(targetPosition, durationSeconds, EasingFunctions.EaseInOutSine));
    }
}
