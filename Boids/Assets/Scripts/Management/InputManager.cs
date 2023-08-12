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
    private Slider mutationChance, mutationFactor, hungryPercentage, reproducePercentage;

    [SerializeField]
    private ToggleGroup flockIDToggleGroup;

    [SerializeField]
    private ToggleGroup colorToggleGroup;

    // From Pop options
    private int numberOfPopulations;
    private int populationIndex;
    [SerializeField]
    private TMP_Text populationNumAsTxt;
    private int startingPopulations = 1;
    private string startingPopTxt = "1";
    public Button upButton, downButton;
    public Toggle togPop1, togPop2, togPop3, togPop4, togPop5, togPop6;

    // Pop options methods
    public void Start()
    {
        downButton.interactable = false;
        numberOfPopulations = startingPopulations;
        populationNumAsTxt.text = startingPopTxt;
    }

    public void IncreasePopulations()
    {
        downButton.interactable = true;
        numberOfPopulations = int.Parse(populationNumAsTxt.text);
        numberOfPopulations++;
        populationNumAsTxt.text = numberOfPopulations.ToString();
        if (numberOfPopulations == 6)
        {
            upButton.interactable = false;

        }
    }

    public void DecreasePopulations()
    {
        upButton.interactable = true;
        numberOfPopulations = int.Parse(populationNumAsTxt.text);
        numberOfPopulations--;
        populationNumAsTxt.text = numberOfPopulations.ToString();
        if (numberOfPopulations == 1)
        {
            downButton.interactable = false;

        }
    }


    public void ActivateOptions()
    {

        switch (populationNumAsTxt.text)
        {
            case "1":
                togPop2.interactable = false;
                togPop3.interactable = false;
                togPop4.interactable = false;
                togPop5.interactable = false;
                togPop6.interactable = false;
                break;
            case "2": togPop2.interactable = true; break;
            case "3": togPop3.interactable = true; break;
            case "4": togPop4.interactable = true; break;
            case "5": togPop5.interactable = true; break;
            case "6": togPop6.interactable = true; break;

        }
    }

    public void DeactivateOptions()
    {

        switch (populationNumAsTxt.text)
        {
            case "5":
                togPop1.isOn = true;
                togPop6.interactable = false; break;
            case "4":
                togPop1.isOn = true;
                togPop5.interactable = false; break;
            case "3":
                togPop1.isOn = true;
                togPop4.interactable = false; break;
            case "2":
                togPop1.isOn = true;
                togPop3.interactable = false; break;
            case "1":
                togPop1.isOn = true;
                togPop2.interactable = false; break;

        }
        GameManager._instance.currPopId = "1";
        SetFlockPopOptionValues(GameManager._instance.currPopId);
    }

    public void OpenPopOptions()
    {
        if (togPop1.isOn == true)
        {
            GameManager._instance.currPopId = "1";
        }
        if (togPop2.isOn == true)
        {
            GameManager._instance.currPopId = "2";
        }
        if (togPop3.isOn == true)
        {
            GameManager._instance.currPopId = "3";
        }
        if (togPop4.isOn == true)
        {
            GameManager._instance.currPopId = "4";
        }
        if (togPop5.isOn == true)
        {
            GameManager._instance.currPopId = "5";
        }
        if (togPop6.isOn == true)
        {
            GameManager._instance.currPopId = "6";
        }
        SetFlockPopOptionValues(GameManager._instance.currPopId);
    }

    private void SetFlockPopOptionValues(string flockId)
    {
        GameManager.Flock flock = GameManager._instance.GetFlockByID(flockId);

        if (flock != null)
        {
            particleInput.text = flock.particleCount.ToString();
            speed.text = flock.speed.ToString();
            sightRadius.text = flock.sightRadius.ToString();
            initEnergy.text = flock.initEnergy.ToString();
            sep.text = flock.separationWeight.ToString();
            ali.text = flock.alignmentWeight.ToString();
            coh.text = flock.cohesionWeight.ToString();
        }
    }

    public float getHungryPercentage()
    {
        //Debug.Log(ValidateEntry.ValidateInput(hungryPercentage.value.ToString("F2"));
        return ValidateEntry.ValidateInput(hungryPercentage.value.ToString("F2"));
    }
    public float getReproducePercentage()
    {
        //Debug.Log(ValidateEntry.ValidateInput(reproducePercentage.value.ToString("F2"));
        return ValidateEntry.ValidateInput(reproducePercentage.value.ToString("F2"));
    }
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
    
    public Color GetSelectedColor()
    {
        foreach (Toggle toggle in colorToggleGroup.ActiveToggles())
        {
            return ConvertStringToColor(toggle.name); // Convert the string name to a Color
        }
        return Color.white; // Return white as a default if no toggle is active
    }

    private Color ConvertStringToColor(string colorName)
    {
        switch (colorName.ToLower())
        {
            case "red":
                return Color.red;
            case "orange":
                return new Color(1, 0.5f, 0); // RGB for Orange
            case "yellow":
                return Color.yellow;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "purple":
                return new Color(0.5f, 0, 0.5f); // RGB for Purple
            default:
                return Color.white; // Default to white if the color name doesn't match
        }
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

        /*
        foreach (Toggle toggle in flockIDToggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }

        foreach (Toggle toggle in colorToggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }
        */
    }
}
