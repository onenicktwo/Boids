using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause_Menu;
    public static bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        Pause_Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }

    public void PauseGame()
    {
        Pause_Menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Pause_Menu.SetActive(false);

    }

    public void GoToMainMenu()
    {
        GameManager._instance.GoToMainMenu();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        GameManager._instance.particleCount = 0;
        
    }    
}