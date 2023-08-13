using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public class Flock
    {
        public string flockID; // Unique identifier for each flock
        public float alignmentWeight;
        public float cohesionWeight;
        public float separationWeight;
        public float initEnergy;
        public float speed; // Added speed to the Flock class
        public float sightRadius; // Added sightRadius to the Flock class
        public int particleCount;
        public Color flockColor;
        public float hungryPercentage;
        public float reproducePercentage;
    }

    public static GameManager _instance;

    private InputManager inputManager;

    public string currPopId = "1";

    [HideInInspector]
    public int particleCount = 0;
    [HideInInspector]
    public int foodCount = 0;
    [HideInInspector]
    public List<GameObject> particles = new List<GameObject>();

    public Hashtable flocks = new Hashtable();

    public int energyFromFood;
    public int foodPerSec;

    public TextMeshProUGUI warningText;

    public int initialFood;

    public float maxX = 11.4f;
    public float maxY = 5f;
    
    public float mutationChance = 0f;
    public float mutationFactor = .1f;

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

    public void AddFlock(string id, float alignmentWeight, float cohesionWeight, float separationWeight, float initEnergy, int particleCount, Color flockColor, float speed, float sightRadius, float hungryPercent, float reproducePercent)
    {
        Flock flock = new Flock
        {
            flockID = id,
            alignmentWeight = alignmentWeight,
            cohesionWeight = cohesionWeight,
            separationWeight = separationWeight,
            initEnergy = initEnergy,
            particleCount = particleCount,
            flockColor = flockColor,
            speed = speed, // Set the speed for the flock
            sightRadius = sightRadius, // Set the sightRadius for the flock
            hungryPercentage = hungryPercent,
            reproducePercentage = reproducePercent
        };

        if (flocks.ContainsKey(id))
            flocks[id] = flock;
        else
            flocks.Add(id, flock);
    }

    public Flock GetFlockByID(string id)
    {
        return (Flock) flocks[id];
    }

    public void Set()
    {
        ValidateEntry.ClearWarning();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        try
        { 
            int initialParticles = (int) inputManager.getParticleInput();
            float alignmentWeight = inputManager.getAlignment();
            float cohesionWeight = inputManager.getCohesion();
            float separationWeight = inputManager.getSeperation();
            float initEnergy = inputManager.getInitEnergy();
            float speed = inputManager.getSpeed(); // Get the speed from InputManager
            float sightRadius = inputManager.getSightRadius(); // Get the sightRadius from InputManager

            float hungryPercentage = inputManager.getHungryPercentage();
            float reproducePercentage = inputManager.getReproducePercentage();

            Color selectedColor = inputManager.GetSelectedColor();

            string selectedFlockID = currPopId;

            AddFlock(selectedFlockID, alignmentWeight, cohesionWeight, separationWeight, initEnergy, initialParticles, selectedColor, speed, sightRadius, hungryPercentage, reproducePercentage);
            Debug.Log(flocks.Count);
        } 
        catch (Exception e)
        {
            ValidateEntry.FlagInvalidEntry();
            Debug.Log(e.ToString());
        }
    }

    public void StartGame()
    {
        ValidateEntry.ClearWarning();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        try
        {
            maxX = inputManager.getMaxX();
            maxY = inputManager.getMaxY();

            initialFood = (int)inputManager.getFoodInput();
            energyFromFood = (int)inputManager.getEnergyInput();
            foodPerSec = (int)inputManager.getFoodPerSecInput();

            mutationChance = inputManager.getMutationChance();
            mutationFactor = inputManager.getMutationFactor();

            if(flocks.Count == 0)
            {
                ValidateEntry.FlagInvalidEntry();
            }
        }
        catch (Exception e)
        {
            ValidateEntry.FlagInvalidEntry();
            Debug.Log(e.ToString());
        }
        if (GameManager._instance.warningText.text == "")
        {
            SceneManager.LoadScene("Game");
        }
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
}