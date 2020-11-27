using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    public static float Volume = 0.5f;
    public Slider slider;

    
 
    public void Update()
    {
        Volume = slider.value;
    }


}
