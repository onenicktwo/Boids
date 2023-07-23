using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public int particleCount;
    public int foodCount;
    public List<GameObject> particles = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();
    public int energyFromFood;   // Holds the energy value of the food

    public TMP_InputField particleInput;
    public TMP_InputField foodInput;
    public TMP_InputField energyInput;    

    public TextMeshProUGUI warningText;
    

    public float maxX = 11.4f;
    public float maxY = 5f;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
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

    public void AddFood(GameObject food)
    {
        foods.Add(food);
        foodCount++;
    }

    public void RemoveFood(GameObject food)
    {
        foods.Remove(food);
        foodCount--;
    }
    
    public void Verify() {
        //Validate input is present and parse into integers
          int initialParticles = ValidateEntry.ValidateInput(particleInput.text);
          int initialFood = ValidateEntry.ValidateInput(foodInput.text);
          energyFromFood = ValidateEntry.ValidateInput(energyInput.text);

        //Checks for and flags invalid entries:

        if (initialParticles == -1 || initialFood == -1 || energyFromFood == -1) {
            ValidateEntry.FlagInvalidEntry();
        } else {
            ValidateEntry.ClearWarning();
            StartGame(initialParticles, initialFood);
        }
    }

    public void StartGame(int particleCount, int foodCount)
    {
        this.particleCount = particleCount;
        this.foodCount = foodCount;

        SceneManager.LoadScene("Game");

    }

    public void EndGame() {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}