// Reference:
//	- https://github.com/Brackeys/Health-Bar
//	- https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;
	public void SetMaxHealth(float health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

    // gradient colors
    public void SetHealth(float health)
	{
		slider.value = health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
