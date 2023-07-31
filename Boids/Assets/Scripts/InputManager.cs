using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TMP_InputField particleInput;
    public TMP_InputField foodInput;
    public TMP_InputField energyInput;
    public TMP_InputField foodPerSecInput;
    public TMP_InputField xAxisInput;
    public TMP_InputField yAxisInput;

    public int getParticleInput()
    {
        return ValidateEntry.ValidateInput(particleInput.text);
    }
    public int getFoodInput()
    {
        return ValidateEntry.ValidateInput(foodInput.text);
    }
    public int getEnergyInput()
    {
        return ValidateEntry.ValidateInput(energyInput.text);
    }
    public int getFoodPerSecInput()
    {
        return ValidateEntry.ValidateInput(foodPerSecInput.text);
    }

    public float getXAxisInput() {
        return ValidateEntry.ValidateX(xAxisInput.text);
    }

    public float getYAxisInput() {
        return ValidateEntry.ValidateY(yAxisInput.text);
    }


    public void ClearInputs()
    {
        particleInput.text = "";
        foodInput.text = "";
        energyInput.text = "";
        foodPerSecInput.text = "";
    }
}
