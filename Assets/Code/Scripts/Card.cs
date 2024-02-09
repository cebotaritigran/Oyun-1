using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public bool facedUp = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnMouseDown()
    {

        if (facedUp == false)
        {
            transform.eulerAngles = new Vector3(180, 0, 0);
            facedUp = true;
        }
        else if (facedUp == true)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facedUp = false;
        }

    }
    void OnMouseExit()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
