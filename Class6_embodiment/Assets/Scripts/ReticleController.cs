using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    public Image RadialBar, Reticle;
    public static bool dwelltoggle;

    // Start is called before the first frame update
    void Start()
    {
        //radial bar starts at 0
        RadialBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if hold down F
        if (Input.GetKey(KeyCode.F))
        {
            //radial bar becomes full
            RadialBar.fillAmount += Time.deltaTime;
        }
    }


    //functions for event system
    public void activateReticle()
    {
        Reticle.color = Color.white;
    }

    public void deactivateReticle()
    {
        Reticle.color = Color.black;
    }

    public void activateRadial()
    {
        RadialBar.fillAmount += Time.deltaTime;
    }

    public void deactivateRadial()
    {
        RadialBar.fillAmount = 0;
    }
}
