using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetter : MonoBehaviour
{
    public static float Volume;
    public Slider slider;

    
 
    public void Update()
    {
        Volume = slider.value;
    }


}
