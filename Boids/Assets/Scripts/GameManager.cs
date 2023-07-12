using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using TMPro; // For input fields

public class GameManager : MonoBehaviour{
    public static GameManager _instance;
    public static GameManager Instance{
        get{
            if(_instance == null){
                Debug.LogError("Game Manager is NULL");
            }
            return _instance;
        }
    }

    public int particleCount;    // Holds the count of particles
    public int foodCount;        // Holds the count of food
    public List<GameObject> particles = new List<GameObject>(); // Holds references to all particles

    // Reference to the input fields in the UI
    public TMP_InputField particleInput; // Using UnityEngine.UI
    public TMP_InputField foodInput;     // Using UnityEngine.UI

    //Reference to warning text area
    public TextMeshProUGUI warningText;
    private void Awake(){
        if (_instance){
            Destroy(gameObject);
        }
        else{
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void AddParticle(GameObject particle){
        // Add a particle to the list
        particles.Add(particle);
        // Increase the count
        particleCount++;
    }

    public void RemoveParticle(GameObject particle){
        // Remove a particle from the list
        particles.Remove(particle);
        // Decrease the count
        particleCount--;
    }

    public void AddFood(){
        // Increase the count
        foodCount++;
    }

    public void RemoveFood(){
        // Decrease the count
        foodCount--;
    }



   

    public void StartGame(){
        //Validate input is present and parse into integers
        int initialParticles = ValidateEntry.ValidateInput(particleInput.text);
        int initialFood = ValidateEntry.ValidateInput(foodInput.text);

        //Checks for and flags invalid entries:
        if (initialParticles == -1 || initialFood == -1) {
            ValidateEntry.FlagInvalidEntry();
        } else {
            ValidateEntry.ClearWarning();
        

            // Store the initial particle and food counts
            PlayerPrefs.SetInt("InitialParticles", initialParticles);
            PlayerPrefs.SetInt("InitialFood", initialFood);

            // Load the game scene
            SceneManager.LoadScene("Game"); // Replace "GameScene" with the name of your main game scene

            //Testing:
            Debug.Log("InitialFood is now " + initialFood + " and InitialParticles is now " + initialParticles);
        }
    }



    public void EndGame(){
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your start menu scene
    }

    public enum GameState { Play, Pause, MainMenu }
    public GameState gameState;

    public void PauseGame(){
        // Implement code to pause game
        gameState = GameState.Pause;
    }

    public void ResumeGame(){
        // Implement code to resume game
        gameState = GameState.Play;
    }

    public void RestartGame(){
        // Implement code to restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}