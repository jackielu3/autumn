using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Start()
    {
        // Temp values, gets set in PlayerHit
        slider.minValue = 0;
        slider.maxValue = 10;
    }

    private void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    private void SetHealth(int health)
    {
        slider.value = health;
    }

    public void UpdateHealth(Component sender, object data)
    {
        if (data is int)
        {
            int amount = (int)data;
            SetHealth(amount);
        }
    }

    public void UpdateMaxHealth(Component sender, object data)
    {
        if (data is int)
        {
            int amount = (int)data;
            SetMaxHealth(amount);

        }
    }
}
