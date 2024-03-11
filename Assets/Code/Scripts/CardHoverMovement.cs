using UnityEngine;

public class CardHoverMovement : MonoBehaviour
{

    //                           _..._
    //                      /MMMMM\
    //                     (I8H#H8I)
    //                     (I8H#H8I)
    //                      \WWWWW/
    //                       I._.I
    //                       I._.I
    //                       I._.I
    //                       I._.I
    //                       I._.I
    //                       I._.I
    //                       I._.I
    //                       I.,.I
    //                      / /#\ \
    //                    .dH# # #Hb.
    //                _.~d#XXP I 7XX#b~,_
    //             _.dXV^XP^ Y X Y ^7X^VXb._
    //            /AP^   \PY   Y   Y7/   ^VA\
    //           /8/      \PP  I  77/      \8\
    //          /J/        IV     VI        \L\
    //          L|         |  \ /  |         |J
    //          V          |  | |  |          V
    //                     |  | |  |
    //                     |  | |  |
    //                     |  | |  |
    //                     |  | |  |
    //  _                  |  | |  |                  _
    // ( \                 |  | |  |                 / )
    //  \ \                |  | |  |                / /
    // ('\ \               |  | |  |               / /`)
    //  \ \ \              |  | |  |              / / /
    // ('\ \ \             |  | |  |             / / /`)
    //  \ \ \ )            |  | |  |            ( / / /
    // ('\ \( )            |  | |  |            ( )/ /`)
    //  \ \ ( |            |  | |  |            | ) / /
    //   \ \( |            |  | |  |            | )/ /
    //    \ ( |            |  | |  |            | ) /
    //     \( |            |   Y   |            | )/
    //      | |            |   |   |            | |
    //      J | ___...~~--'|   |   |`--~~...___ | L
    //      >-+<...___     |   |   |     ___...>+-<
    //     /     __   `--~.L___L___J.~--'   __     \
    //     K    /  ` --.     d===b     .-- '  \    H
    //     \_._/        \   // I \\   /        \_._/
    //       `--~.._     \__\\ I //__/     _..~--'
    //              `--~~..____ ____..~~--'
    //                     |   T   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     |   |   |
    //                     I   '   I
    //                      \     /
    //                       \   /
    //                        \ /
    private bool ignorePointer = false;
    private const float hoverYCoordinate = 0.15f;

    public void DisableHoverEffect()
    {
        if (ignorePointer)
        {
            // ALREADY DISABLED
            return;
        }
        ignorePointer = true;
        StartCoroutine(transform.AnimateToPositionYAxis(0.0f, 0.15f, EasingFunctions.EaseOutSine));
    }

    public void EnableHoverEffect()
    {
        // ALREADY ENABLED
        if (!ignorePointer)
        {
            return;
        }
        ignorePointer = false;
    }

    void OnMouseEnter()
    {
        if (ignorePointer || !ScoreManager.instance.timerRunning)
        {
            return;
        }

        // CARD HIGHLIGHT (POP) BY CHANGING ITS SCALE WITH VECTOR3
        StartCoroutine(transform.AnimateToPositionYAxis(hoverYCoordinate, 0.15f, EasingFunctions.EaseOutSine));
    }

    void OnMouseOver()
    {
        if (ignorePointer || !ScoreManager.instance.timerRunning)
        {
            return;
        }

        // ROTATION START
        RotateGameObjectBasedWhereMousePoints();
    }

    void OnMouseExit()
    {
        // CARD HIGHLIGH (POP) TO NORMAL BY CHANGING ITS SCALE WITH VECTOR3
        StartCoroutine(transform.AnimateToPositionYAxis(0.0f, 0.15f, EasingFunctions.EaseOutSine));

        // Restart the cards rotation to start so it looks very natural when mouse exits :) maybe not idk
        //                          .                                               
        //                      /   ))     |\         )               ).           
        //                c--. (\  ( `.    / )  (\   ( `.     ).     ( (           
        //                | |   ))  ) )   ( (   `.`.  ) )    ( (      ) )          
        //                | |  ( ( / _..----.._  ) | ( ( _..----.._  ( (           
        //  ,-.           | |---) V.'-------.. `-. )-/.-' ..------ `--) \._        
        //  | /===========| |  (   |      ) ( ``-.`\/'.-''           (   ) ``-._   
        //  | | / / / / / | |--------------------->  <-------------------------_>=-
        //  | \===========| |                 ..-'./\.`-..                _,,-'    
        //  `-'           | |-------._------''_.-'----`-._``------_.-----'         
        //                | |         ``----''            ``----''                  
        //                | |                                                       
        //                c--` 
        //transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(transform.AnimateRotation(transform.rotation, Quaternion.Euler(0, 0, 0), 0.15f, EasingFunctions.EaseOutSine));
    }

    void OnMouseDown()
    {
        if (ScoreManager.instance.timerRunning && !GameManager.instance.twoCardsPicked)
        {
            Card hitCard = gameObject.GetComponent<Card>();
            if (!hitCard.facedUp)
            {
                // TO NOT REGISTER DOUBLE CLICK AS A MATCH
                hitCard.FlipUp(true);
                GameManager.instance.AddCardToPickedList(hitCard);
            }
        }
    }

    // ROTATE GAME OBJECT BASED ON WHERE MOUSE POINTS
    // using raycast and inverse trans form point to get local position of game object
    // on which mouse points to get a value like 7f and -7f depends on where mouse points
    private Transform RotateGameObjectBasedWhereMousePoints()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("found " + hit.collider.gameObject + " at distance: " + hit.collider.gameObject.transform.localPosition);
        }

        // getting local position where mouse points
        Vector3 localHit = transform.InverseTransformPoint(hit.point);


        // ROTATE X coordinate CARD ON MOUSE HOVER
        // in this case I'm using Z axis because its foward and back where mouse points
        // fro exmple if mouse at the head of the card its 7f and if its on the back of the cards itts -7f
        // using it we can rotate X axis to make it look that it tilts foward or back
        // Debug.Log(localHit.z);
        float currentRotation = 0f;
        float startRotationXPlus = 0f;
        float endRotationXPlus = 10f;

        // the min rotation can x axis be
        float startRotationXMinus = -10f;
        // the max rotation can x axis be
        float endRotationXMinus = 1f;

        if (localHit.z > 0.1f)
        {
            currentRotation = Mathf.Clamp(localHit.z * endRotationXPlus, startRotationXPlus, endRotationXPlus);
            transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
        }
        else if (localHit.z < -0.1f)
        {
            currentRotation = Mathf.Clamp(localHit.z * endRotationXPlus, startRotationXMinus, endRotationXMinus);
            transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
        }



        // ROTATE Z coordinate CARD ON MOUSE HOVER
        //Debug.Log(localHit.x);

        float currentRotationZ = 0f;
        float startRotationZPlus = 0f;
        float endRotationZPlus = 10f;


        // the min rotation can x axis be
        float startRotationZMinus = -10f;
        // the max rotation can x axis be
        float endRotationZMinus = 1f;

        if (localHit.x > 0.05f)
        {
            currentRotationZ = Mathf.Clamp(-localHit.x * endRotationZPlus, startRotationZMinus, endRotationZMinus);
            transform.rotation = Quaternion.Euler(currentRotation, 0, currentRotationZ);
        }
        else if (localHit.x < -0.05f)
        {
            currentRotationZ = Mathf.Clamp(localHit.x * -endRotationZPlus, startRotationZPlus, endRotationZPlus);
            transform.rotation = Quaternion.Euler(currentRotation, 0, currentRotationZ);
        }



        return hit.transform;
    }


    // Update is called once per frame
    void Update()
    {


    }
}
