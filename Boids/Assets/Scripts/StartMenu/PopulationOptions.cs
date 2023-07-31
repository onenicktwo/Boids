using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopulationOptions : MonoBehaviour {

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
    public GameObject PopOpts1;
    public GameObject PopOpts2;
    public GameObject PopOpts3;
    public GameObject PopOpts4;
    public GameObject PopOpts5;
    public GameObject PopOpts6;


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

    public void ResetPopOpts() {
        PopOpts1.SetActive(true);
        PopOpts2.SetActive(false);
        PopOpts3.SetActive(false);
        PopOpts4.SetActive(false);
        PopOpts5.SetActive(false);
        PopOpts6.SetActive(false);
    }

    public void OpenPopOptions() {
            ResetPopOpts();
        if (togPop2.isOn == true) {
            PopOpts1.SetActive(false);
            PopOpts2.SetActive(true);
        }
        if (togPop3.isOn == true) {
            PopOpts1.SetActive(false);
            PopOpts3.SetActive(true);
        }
        if (togPop4.isOn == true) {
            PopOpts1.SetActive(false);
            PopOpts4.SetActive(true);
        }
        if (togPop5.isOn == true) {
            PopOpts1.SetActive(false);
            PopOpts5.SetActive(true);
        }
        if (togPop6.isOn == true) {
            PopOpts1.SetActive(false);
            PopOpts6.SetActive(true);
        }
    }

}
