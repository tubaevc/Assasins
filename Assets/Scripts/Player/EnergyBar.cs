using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Slider energyBarSlider;

    public void GiveFullEnergy(float energy)
    {
        energyBarSlider.maxValue = energy;
        energyBarSlider.value = energy;
        
    }

    public void SetEnergy(float energy)
    {
        energyBarSlider.value = energy;
    }
}
