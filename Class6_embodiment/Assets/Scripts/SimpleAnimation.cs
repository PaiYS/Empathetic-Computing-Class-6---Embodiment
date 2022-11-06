using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimation : MonoBehaviour
{
    public float speed; //how fast you want the animation to be
    public float finalX; //the final x point
    Vector3 startPosition; // the start position
    Vector3 endPosition; // the end position
    Vector3 newPosition; // the new position for the movement

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;  //the start position is whatever the position of the game object is at the beginning
        endPosition = new Vector3(finalX, startPosition.y, startPosition.z);  // the end position is the new finalZ value
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == startPosition)
        {
            newPosition = endPosition;
        }
        if (transform.position == endPosition)
        {
            newPosition = startPosition;
        }

        //moves the game object from wherever it is currently at to the new position at the defined speed linearly
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed);
    }
}
