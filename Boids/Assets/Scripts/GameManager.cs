using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
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

    private void Awake(){
        if (_instance){
            Destroy(gameObject);
        }
        else{
            _instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void AddParticle(GameObject particle)
    {
        // Add a particle to the list
        particles.Add(particle);
        // Increase the count
        particleCount++;
    }

    public void RemoveParticle(GameObject particle)
    {
        // Remove a particle from the list
        particles.Remove(particle);
        // Decrease the count
        particleCount--;
    }

    public void AddFood()
    {
        // Increase the count
        foodCount++;
    }

    public void RemoveFood()
    {
        // Decrease the count
        foodCount--;
    }

    // Reference to the input fields in the UI
    public TextField particleInput;
    public TextField foodInput;

    public void StartGame()
    {
        // Parse the text in the input fields to get the initial values
        int initialParticles = int.Parse(particleInput.text);
        int initialFood = int.Parse(foodInput.text);

        // Call methods to spawn initial particles and food
        SpawnParticles(initialParticles);
        SpawnFood(initialFood);
    }
}