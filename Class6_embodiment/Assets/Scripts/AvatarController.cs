using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes the avatar rotation about y follows the camera
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //keeps the local position of the avatar to not deviate
        transform.localPosition = new Vector3(0, transform.localPosition.y, 0);
    }
}
