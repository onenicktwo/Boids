using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager _instance;
    public TMP_InputField particleInput;
    public TMP_InputField foodInput;
    public TMP_InputField energyInput;
    public TMP_InputField foodPerSecInput;
    public TMP_InputField alignmentWeightInput;
    public TMP_InputField cohesionWeightInput;
    public TMP_InputField separationWeightInput;
    public GameManager gameManager;
    public TMP_InputField populationChoices;
    public Button setButton;

    private List<Button> populationButtons = new List<Button>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //listeners
        particleInput.onEndEdit.AddListener(delegate { gameManager.UpdateParticleCount(getParticleInput()); });
        foodInput.onEndEdit.AddListener(delegate { gameManager.UpdateFoodCount(getFoodInput()); });
        energyInput.onEndEdit.AddListener(delegate { gameManager.UpdateEnergyFromFood(getEnergyInput()); });
        foodPerSecInput.onEndEdit.AddListener(delegate { gameManager.UpdateFoodPerSec(getFoodPerSecInput()); });
        alignmentWeightInput.onEndEdit.AddListener(delegate { EditCurrentFlock(); });
        cohesionWeightInput.onEndEdit.AddListener(delegate { EditCurrentFlock(); });
        separationWeightInput.onEndEdit.AddListener(delegate { EditCurrentFlock(); });
        setButton.onClick.AddListener(SaveCurrentPopulation);

        // Populate the list of population buttons
        foreach (Transform child in populationList)
        {
            Button btn = child.GetComponent<Button>();
            if (btn)
            {
                populationButtons.Add(btn);
                btn.onClick.AddListener(() => LoadSelectedPopulation(populationButtons.IndexOf(btn)));
            }
        }

        UpdatePopulationList(); // Call this method to initialize the list
    }

    public void SaveCurrentPopulation()
    {
        gameManager.SaveCurrentPopulation();
        UpdatePopulationList();
    }

    public void LoadSelectedPopulation(int index)
    {
        gameManager.LoadPopulation(index);
        UpdateInputFields();
    }

    public void UpdatePopulationList()
    {
        for (int i = 0; i < populationButtons.Count; i++)
        {
            if (i < gameManager.flocks.Count)
            {
                populationButtons[i].gameObject.SetActive(true);
                populationButtons[i].GetComponentInChildren<Text>().text = "Population " + (i + 1);
            }
            else
            {
                populationButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void EditCurrentFlock()
    {
        float alignmentWeight = float.Parse(alignmentWeightInput.text);
        float cohesionWeight = float.Parse(cohesionWeightInput.text);
        float separationWeight = float.Parse(separationWeightInput.text);
        int particleCount = getParticleInput();
        int energyFromFood = getEnergyInput();
        int foodPerSec = getFoodPerSecInput();

        gameManager.EditCurrentFlock(alignmentWeight, cohesionWeight, separationWeight, energyFromFood, particleCount);
    }

    public void UpdateInputFields()
    {
        if (gameManager.currentFlock != null)
        {
            alignmentWeightInput.text = gameManager.currentFlock.alignmentWeight.ToString();
            cohesionWeightInput.text = gameManager.currentFlock.cohesionWeight.ToString();
            separationWeightInput.text = gameManager.currentFlock.separationWeight.ToString();
            energyInput.text = gameManager.currentFlock.initEnergy.ToString();
            particleInput.text = gameManager.currentFlock.particleCount.ToString();
        }
    }

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

    public void ClearInputs()
    {
        particleInput.text = "";
        foodInput.text = "";
        energyInput.text = "";
        foodPerSecInput.text = "";
    }
}