using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthslider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
  

    public void Sethealth(int hp)
    {
        healthslider.value = hp;
        fill.color = gradient.Evaluate(healthslider.normalizedValue);
    }


    public void SetMaxHealth(int hp)
    {
        healthslider.maxValue = hp;
        healthslider.value = hp;
        fill.color = gradient.Evaluate(1f);
    }
}
