using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public event Action OnDieAction;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void OnDamage(float damage)
    {
        slider.value -= damage;
        if (slider.value <= 0)
        {
            //if (OnDieAction != null)
            {
                OnDieAction();
            }
        }
    }
}
