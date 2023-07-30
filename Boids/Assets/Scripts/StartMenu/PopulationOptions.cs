using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopulationOptions : MonoBehaviour {

    public int numberOfPopulations;    
    public int populationIndex;
    public TMP_Text populationNumAsTxt;
    public int startingPopulations = 1;
    public string startingPopTxt = "1";
    public Button upButton;
    public Button downButton;
    

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

}
