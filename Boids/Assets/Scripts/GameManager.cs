using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public class Flock
    {
        public float alignmentWeight;
        public float cohesionWeight;
        public float separationWeight;
        public float initEnergy;
        public int particleCount;
    }

    public static GameManager _instance;

    public InputManager inputManager;

    [HideInInspector]
    public int particleCount = 0;
    [HideInInspector]
    public int foodCount = 0;
    [HideInInspector]
    public List<GameObject> particles = new List<GameObject>();
    public List<Flock> flocks = new List<Flock>();
    public int energyFromFood;
    public int foodPerSec;

    public TextMeshProUGUI warningText;

    public int initialParticles;
    public int initialFood;

    public float maxX = 11.4f;
    public float maxY = 5f;

    public Flock currentFlock;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddParticle(GameObject particle)
    {
        particles.Add(particle);
        particleCount++;
    }

    public void RemoveParticle(GameObject particle)
    {
        particles.Remove(particle);
        particleCount--;
    }

    public void AddFood()
    {
        foodCount++;
    }

    public void RemoveFood()
    {
        foodCount--;
    }

    public void AddFlock(float alignmentWeight, float cohesionWeight, float separationWeight, float initEnergy, int particleCount)
    {
        Flock flock = new Flock
        {
            alignmentWeight = alignmentWeight,
            cohesionWeight = cohesionWeight,
            separationWeight = separationWeight,
            initEnergy = initEnergy,
            particleCount = particleCount
        };

        flocks.Add(flock);
        currentFlock = flock;
    }

    public void SwitchToFlock(int index)
    {
        if (index >= 0 && index < flocks.Count)
        {
            currentFlock = flocks[index];
        }
    }

    public void EditCurrentFlock(float alignmentWeight, float cohesionWeight, float separationWeight, float initEnergy, int particleCount)
    {
        if (currentFlock != null)
        {
            currentFlock.alignmentWeight = alignmentWeight;
            currentFlock.cohesionWeight = cohesionWeight;
            currentFlock.separationWeight = separationWeight;
            currentFlock.initEnergy = initEnergy;
            currentFlock.particleCount = particleCount;
        }
    }

    public void SaveCurrentPopulation()
    {
        // Check if the currentFlock is null, if so, create a new flock
        if (currentFlock == null)
        {
            currentFlock = new Flock();
            flocks.Add(currentFlock);
        }

        // Update the currentFlock's properties based on the input fields
        currentFlock.alignmentWeight = float.Parse(InputManager._instance.alignmentWeightInput.text);
        currentFlock.cohesionWeight = float.Parse(InputManager._instance.cohesionWeightInput.text);
        currentFlock.separationWeight = float.Parse(InputManager._instance.separationWeightInput.text);
        currentFlock.initEnergy = InputManager._instance.getEnergyInput();
        currentFlock.particleCount = InputManager._instance.getParticleInput();

        InputManager._instance.UpdatePopulationList(); // Update the list after saving
    }


    public void LoadPopulation(int index)
    {
        if (index >= 0 && index < flocks.Count)
        {
            currentFlock = flocks[index];

            // Update the input fields based on the currentFlock's properties
            inputManager.alignmentWeightInput.text = currentFlock.alignmentWeight.ToString();
            inputManager.cohesionWeightInput.text = currentFlock.cohesionWeight.ToString();
            inputManager.separationWeightInput.text = currentFlock.separationWeight.ToString();
            inputManager.energyInput.text = currentFlock.initEnergy.ToString();
            inputManager.particleInput.text = currentFlock.particleCount.ToString();
        }
    }

    public void Verify()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        initialParticles = inputManager.getParticleInput();
        initialFood = inputManager.getFoodInput();
        energyFromFood = inputManager.getEnergyInput();
        foodPerSec = inputManager.getFoodPerSecInput();

        if (initialFood <= 0 || energyFromFood <= 0 || initialParticles <= 0 || foodPerSec <= 0)
        {
            ValidateEntry.FlagInvalidEntry();
        }
        else
        {
            ValidateEntry.ClearWarning();
            StartGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void OpenOptions()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void SetOptions()
    {
        CloseOptions();
    }

    public void CloseOptions()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public enum GameState { Play, Pause }
    public GameState gameState;

    public void PauseGame()
    {
        gameState = GameState.Pause;
    }

    public void ResumeGame()
    {
        gameState = GameState.Play;
    }

    public void RestartGame()
    {
        particleCount = 0;
        foodCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

      public void UpdateParticleCount(int newCount)
    {
        initialParticles = newCount;
    }

    public void UpdateFoodCount(int newCount)
    {
        initialFood = newCount;
    }

    public void UpdateEnergyFromFood(int newEnergy)
    {
        energyFromFood = newEnergy;
    }

    public void UpdateFoodPerSec(int newFoodPerSec)
    {
        foodPerSec = newFoodPerSec;
    }

}