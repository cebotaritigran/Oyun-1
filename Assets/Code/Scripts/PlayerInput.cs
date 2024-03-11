using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0) && ScoreManager.instance.timerRunning && !GameManager.instance.twoCardsPicked)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Card hitCard = hit.transform.GetComponent<Card>();
                if (!hitCard.facedUp)
                {
                    // TO NOT REGISTER DOUBLE CLICK AS A MATCH
                    hitCard.FlipUp(true);
                    GameManager.instance.AddCardToPickedList(hitCard);
                }
            }
        }
    }*/
}
