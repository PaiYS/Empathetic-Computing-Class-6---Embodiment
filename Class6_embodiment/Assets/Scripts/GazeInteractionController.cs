using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.XR.Cardboard;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class GazeInteractionController : MonoBehaviour
{
    public float interactableDistance = 100;
    public GameObject gazedObject;
    public PointerEventData _eventData;

    public NavMeshAgent playerNavMeshAgent;
    public GameObject marker;
    public float minMarkerDistance = 2;

    public UnityEvent onFocus;
    public UnityEvent onLoseFocus;
    public UnityEvent onDwell;
    public UnityEvent onLoseDwell;

    public GameObject avatar;

    enum NavigationMethod {NavMesh, Auto, Teleport }
    [SerializeField] NavigationMethod navigationMethod;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {
        //shoots a laser from the middle at a distance of interactableDistance
        if (Physics.Raycast(transform.position, transform.forward, out var hit, interactableDistance))
        {
            //if it hits a gameobject, gazedobject is referred to as that game object
            gazedObject = hit.transform.gameObject;
            //Debug.Log(gazedObject);
            //reticle color is white
            onFocus.Invoke();

            //if the cardboard button or mouse left click is held down
            if (Api.IsTriggerHeldPressed || Input.GetMouseButton(0))
            {
                //radial bar will fill in time
                onDwell.Invoke();
            }
            else
            {
                //radial bar returns to 0
                onLoseDwell.Invoke();
            }

            //if navmesh is selected
            if (navigationMethod == NavigationMethod.NavMesh)
            {
                if (playerNavMeshAgent.remainingDistance <= playerNavMeshAgent.stoppingDistance)
                {
                    //play idle animation
                    avatar.GetComponent<Animator>().SetBool("isWalking", false);
                }
                else
                {
                    //play walking animation
                    avatar.GetComponent<Animator>().SetBool("isWalking", true);
                }

                //if just press cardboard button once or click once
                if (Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
                {
                    if (gazedObject.layer == LayerMask.NameToLayer("ground"))
                    {
                        //marker moves to clicked position
                        marker.transform.position = hit.point;
                        //player moves to clicked position
                        playerNavMeshAgent.SetDestination(hit.point);
                    }
                }

                //checkes distance between player and marker. if the player is far from marker...
                if (Vector3.Distance(marker.transform.position, playerNavMeshAgent.transform.position) > minMarkerDistance)
                {
                    //we can see the marker
                    marker.SetActive(true);
                }
                else //otherwise,
                {
                    //we can't see the marker
                    marker.SetActive(false);
                }
            }
            //if auto is selected
            else if (navigationMethod == NavigationMethod.Auto)
            {
                //deactivate the marker
                marker.SetActive(false);

                float angle = 30, speed = 3;
                bool moveForward;
                Vector3 forward;
                CharacterController cc = GameObject.Find("Player").GetComponent<CharacterController>();

                if (GameObject.Find("Player").GetComponent<CharacterController>() == null)
                {
                    //creates a charactercontroller component, essential for this method to move
                    cc = GameObject.Find("Player").AddComponent<CharacterController>();
                    cc.height = 3.3f;
                }
                if (transform.eulerAngles.x >= angle && transform.eulerAngles.x < 90) //checks if the head downward angle is between 30 and 90 degrees
                {
                    moveForward = true;
                    //play walking animation
                    avatar.GetComponent<Animator>().SetBool("isWalking", true);
                }
                else
                {
                    moveForward = false;
                    //play idle animation
                    avatar.GetComponent<Animator>().SetBool("isWalking", false);
                }

                if (moveForward)
                {
                    forward = transform.TransformDirection(Vector3.forward); //current direction is the forward direction
                    cc.SimpleMove(forward * speed); //moves the character according to forward direction at a designated speed
                }
            }
            //if teleport is selected
            else if (navigationMethod == NavigationMethod.Teleport)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("teleportpoint"))
                {
                    if (Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
                    {
                        GameObject.Find("Player").transform.position = hit.point;
                    }

                    //checkes distance between player and teleportpoints. if the player is far from marker...
                    if (Vector3.Distance(hit.transform.position, playerNavMeshAgent.transform.position) > minMarkerDistance)
                    {
                        //every gameobject that is a child of TeleportPoints will be visible
                        foreach (Transform child in GameObject.Find("TeleportPoints").transform)
                        {
                            child.transform.gameObject.SetActive(true);
                        }
                    }
                    else //otherwise,
                    {
                        //we can't see the teleport point the player is standing at
                        hit.transform.gameObject.SetActive(false);
                    }
                }
            }
            
        }
        else
        {
            //gazedobject is empty
            gazedObject = null;
            //reticle color is black
            onLoseFocus.Invoke();
        }
    }
}
