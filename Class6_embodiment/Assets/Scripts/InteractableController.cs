using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.XR.Cardboard;
using UnityEngine.UI;

public class InteractableController : MonoBehaviour
{
    public Image radialBar;
    public GameObject interactableObject;
    public Transform PlayerTransform;
    public Transform interactables;
    public float interactableDistance = 10;
    public bool picked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fire a raycast to hit an object
        if (Physics.Raycast(transform.position, transform.forward, out var hit, interactableDistance))
        {
            interactableObject = hit.transform.gameObject;
        }

        // if there is an object, it is interactable, currently not picking anything, the object is really close to the player
        if (interactableObject != null && interactableObject.layer == 7 && picked == false && Vector3.Distance(PlayerTransform.position, interactableObject.transform.position) < 2.2f)
        {
            if (radialBar.fillAmount == 1)
            {
                // the rigid body become fully in control by this script and not by the physics engine
                interactableObject.GetComponent<Rigidbody>().isKinematic = true;
                // the object becomes a child of playertransform, and moves according to the parent
                interactableObject.transform.SetParent(PlayerTransform);
                picked = true;
            }

        }

        // if there is an object, currently picking something and the button is pressed      
        else if (interactableObject != null && picked == true)
        {
            if (radialBar.fillAmount == 0)
            {
                //the rigid body is free again
                interactableObject.GetComponent<Rigidbody>().isKinematic = false;
                // the object is not a child of player transform anymore
                PlayerTransform.DetachChildren();
                // th object returns to the child of intractable
                interactableObject.transform.SetParent(interactables);
                picked = false;
            }
        }
    }
}
