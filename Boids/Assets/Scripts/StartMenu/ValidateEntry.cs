using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For input fields

public class ValidateEntry : MonoBehaviour {

    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text resultText;

    public static int ValidateInput(string inputString) {
        //string inputString = inputField.text;
        if (inputString != "" && inputString != "-") {
            int inputInt = int.Parse(inputString);
            if(inputInt > 0) {
                return inputInt;
            } else {
                return -1;
            }
        } else {
            return -1;
        }
    }



  public static void FlagInvalidEntry() {
      GameManager._instance.warningText.text = "Invalid Entry. Please enter a positive, whole number for both fields.";
      GameManager._instance.particleInput.text = "";
      GameManager._instance.foodInput.text = "";
  }

  public static void ClearWarning() {
      GameManager._instance.warningText.text = "";
  }

  
}
