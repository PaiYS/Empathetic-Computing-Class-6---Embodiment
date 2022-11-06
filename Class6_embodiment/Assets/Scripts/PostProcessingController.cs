using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    public PostProcessVolume m_Volume;
    public Vignette m_Vignette;

    // Start is called before the first frame update
    void Start()
    {
        m_Vignette = m_Volume.profile.GetSetting<Vignette>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Vignette.intensity.value = 0.9f;
        }
    }
}
