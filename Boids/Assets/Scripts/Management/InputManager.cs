using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField maxX, maxY;

    [SerializeField]
    private TMP_InputField particleInput, speed, sightRadius, initEnergy, sep, ali, coh;

    [SerializeField]
    private TMP_InputField foodInput, energyInput, foodPerSecInput;

    [SerializeField]
    private Slider mutationChance, mutationFactor;

    public float getMutationFactor()
    {
        //Debug.Log(ValidateEntry.ValidateInput(mutationFactor.value.ToString("F2")));
        return ValidateEntry.ValidateInput(mutationFactor.value.ToString("F2"));
    }
    public float getMutationChance()
    {
        //Debug.Log(ValidateEntry.ValidateInput((mutationChance.value * 100).ToString("F0")));
        return ValidateEntry.ValidateInput((mutationChance.value * 100).ToString("F0"));
    }
    public float getMaxY()
    {
       //Debug.Log(ValidateEntry.ValidateInput(maxY.text));
        return ValidateEntry.ValidateInput(maxY.text);
    }
    public float getMaxX()
    {
        //Debug.Log(ValidateEntry.ValidateInput(maxX.text));
        return ValidateEntry.ValidateInput(maxX.text);
    }
    public float getParticleInput()
    {
        //Debug.Log(ValidateEntry.ValidateInput(particleInput.text));
        return ValidateEntry.ValidateInput(particleInput.text);
    }
    public float getSightRadius()
    {
        //Debug.Log(ValidateEntry.ValidateInput(sightRadius.text));
        return ValidateEntry.ValidateInput(sightRadius.text);
    }
    public float getSpeed()
    {
        //Debug.Log(ValidateEntry.ValidateInput(speed.text));
        return ValidateEntry.ValidateInput(speed.text);
    }
    public float getInitEnergy()
    {
        //Debug.Log(ValidateEntry.ValidateInput(initEnergy.text));
        return ValidateEntry.ValidateInput(initEnergy.text);
    }
    public float getSeperation()
    {
        //Debug.Log(ValidateEntry.ValidateInput(sep.text));
        return ValidateEntry.ValidateInput(sep.text);
    }
    public float getAlignment()
    {
        //Debug.Log(ValidateEntry.ValidateInput(ali.text));
        return ValidateEntry.ValidateInput(ali.text);
    }
    public float getCohesion()
    {
        //Debug.Log(ValidateEntry.ValidateInput(coh.text));
        return ValidateEntry.ValidateInput(coh.text);
    }
    public float getFoodInput()
    {
        //Debug.Log(ValidateEntry.ValidateInput(foodInput.text));
        return ValidateEntry.ValidateInput(foodInput.text);
    }
    public float getEnergyInput()
    {
        //Debug.Log(ValidateEntry.ValidateInput(energyInput.text));
        return ValidateEntry.ValidateInput(energyInput.text);
    }
    public float getFoodPerSecInput()
    {
        //Debug.Log(ValidateEntry.ValidateInput(foodPerSecInput.text));
        return ValidateEntry.ValidateInput(foodPerSecInput.text);
    }

    public void ClearAllInputs()
    {
        particleInput.text = "";
        foodInput.text = "";
        energyInput.text = "";
        foodPerSecInput.text = "";
        maxX.text = "";
        maxY.text = "";
        speed.text = "";
        sep.text = "";
        ali.text = "";
        coh.text = "";
        initEnergy.text = "";
        sightRadius.text = "";
    }
}
