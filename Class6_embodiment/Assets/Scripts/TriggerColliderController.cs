using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerColliderController : MonoBehaviour
{
    public TextMeshPro triggerBoard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger entered");
        triggerBoard.text = "welcome!";
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trigger exit");
        triggerBoard.text = "come here";
    }
}
