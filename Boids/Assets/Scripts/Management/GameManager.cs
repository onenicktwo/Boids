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

    private InputManager inputManager;

    [HideInInspector]
    public int particleCount = 0;
    [HideInInspector]
    public int foodCount = 0;
    [HideInInspector]
    public List<GameObject> particles = new List<GameObject>();
    public List<Flock> flocks = new List<Flock>(); // List to store multiple flocks
    public int energyFromFood;
    public int foodPerSec;

    public TextMeshProUGUI warningText;

    public int initialParticles;
    public int initialFood;

    public float maxX = 11.4f;
    public float maxY = 5f;
    
    // 0 < x < 100
    public float mutationChance = 0f;
    // 0 < x < 1
    public float mutationFactor = .1f;

    // Moved it here since messing with spawn rates while reproducing doesn't sound like a good idea
    public float busyTimeFactor = 1f;
    public float reproduceCooldownFactor = 1f;
    public float matureCooldownFactor = 1f;

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
    }

    public void Verify()
    {

        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        initialParticles = inputManager.getParticleInput();
        initialFood = inputManager.getFoodInput();
        energyFromFood = inputManager.getEnergyInput();
        foodPerSec = inputManager.getFoodPerSecInput();

        //Checks for and flags invalid entries:
        if (initialFood <= 0 || 
            energyFromFood <= 0 || 
            initialParticles <= 0 || 
            foodPerSec <= 0) {
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

    public void OpenOptions() {
        SceneManager.LoadSceneAsync(2);
    }

    public void SetOptions() {
        CloseOptions();
    }

    public void CloseOptions() {
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

    public void QuitGame() {
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