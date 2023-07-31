using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [HideInInspector]
    public int particleCount = 0;
    [HideInInspector]
    public int foodCount = 0;
    [HideInInspector]
    public List<GameObject> particles = new List<GameObject>();
    public int energyFromFood;
    public int foodPerSec;

    public TMP_InputField particleInput;
    public TMP_InputField foodInput;
    public TMP_InputField energyInput;
    public TMP_InputField foodPerSecInput;

    public TextMeshProUGUI warningText;

    public int initialParticles;
    public int initialFood;

    public float maxX = 11.4f;
    public float maxY = 5f;

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

    public void AddFood(GameObject food)
    {
        foodCount++;
    }

    public void RemoveFood(GameObject food)
    {
        foodCount--;
    }
    
    public void Verify() {
        //Validate input is present and parse into integers
          initialParticles = ValidateEntry.ValidateInput(particleInput.text);
          initialFood = ValidateEntry.ValidateInput(foodInput.text);
          energyFromFood = ValidateEntry.ValidateInput(energyInput.text);
          foodPerSec = ValidateEntry.ValidateInput(foodPerSecInput.text);

        //Checks for and flags invalid entries:
        if (initialFood <= 0 || 
            energyFromFood <= 0 || 
            initialParticles <= 0 || 
            foodPerSec <= 0) {
            ValidateEntry.FlagInvalidEntry();
        } else {
            ValidateEntry.ClearWarning();
            StartGame();
        }
    }

    public void StartGame()
    {
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
        particleCount = 0;
        foodCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}