using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopulationOptions : MonoBehaviour {
    /*
     * On population change -> 
     * Send a message to Input manager ->
     * Input manager will get the correct flock values stored in the hashmap ->
     * Which will be used to switch out the current values in the related fields in the Input Manager
     * 

     */

    public InputManager inputManager;

    public int numberOfPopulations;    
    public int populationIndex;
    public TMP_Text populationNumAsTxt;
    public TMP_Text toggleName;
    public int startingPopulations = 1;
    public string startingPopTxt = "1";
    public Button upButton;
    public Button downButton;
    public Toggle togPop1;
    public Toggle togPop2;
    public Toggle togPop3;
    public Toggle togPop4;
    public Toggle togPop5;
    public Toggle togPop6;

    public void Start() {
        downButton.interactable = false;
        numberOfPopulations = startingPopulations;
        populationNumAsTxt.text = startingPopTxt;
    }

    public void IncreasePopulations() {
        downButton.interactable = true;
        numberOfPopulations = int.Parse(populationNumAsTxt.text);
        numberOfPopulations++;
        populationNumAsTxt.text = numberOfPopulations.ToString();
        if (numberOfPopulations == 6) {
            upButton.interactable = false;
                
        }
    }

    public void DecreasePopulations() {
        upButton.interactable = true;
        numberOfPopulations = int.Parse(populationNumAsTxt.text);
        numberOfPopulations--;
        populationNumAsTxt.text = numberOfPopulations.ToString();
        if (numberOfPopulations == 1) {
            downButton.interactable = false;

        }
    }


    public void ActivateOptions() {

        switch (populationNumAsTxt.text) {
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

    public void DeactivateOptions() {

        switch (populationNumAsTxt.text) {
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
    }

    public void OpenPopOptions() {
        if (togPop1.isOn == true) {
            GameManager._instance.currPopId = "1";
        }
        if (togPop2.isOn == true) {
            GameManager._instance.currPopId = "2";
        }
        if (togPop3.isOn == true) {
            GameManager._instance.currPopId = "3";
        }
        if (togPop4.isOn == true) {
            GameManager._instance.currPopId = "4";
        }
        if (togPop5.isOn == true) {
            GameManager._instance.currPopId = "5";
        }
        if (togPop6.isOn == true) {
            GameManager._instance.currPopId = "6";
        }
    }

}
