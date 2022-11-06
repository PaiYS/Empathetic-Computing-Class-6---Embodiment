using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public Color thisColor;

    // Start is called before the first frame update
    void Start()
    {
        thisColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

        PressingAButton();
    }

    //a function to create a console log when pressing spacebar
    public void PressingAButton()
    {
        //if user holds down the spacebar
        if (Input.GetKey(KeyCode.Space))
        {
            //the console will print the result
            Debug.Log("pressed space!");

            //change the color of the material instance
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            //change back to original color
            GetComponent<Renderer>().material.color = thisColor;
        }
    }

}
