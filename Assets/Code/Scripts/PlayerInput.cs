using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.timerRunning && !GameManager.instance.twoCardsPicked)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.gameObject);
                Card hitCard = hit.transform.GetComponent<Card>();
                if (!hitCard.facedUp)
                {
                    // TO NOT REGISTER DOUBLE CLICK AS A MATCH
                    hitCard.FlipUp(true);
                    GameManager.instance.AddCardToPickedList(hitCard);
                }
            }
        }
    }
}
