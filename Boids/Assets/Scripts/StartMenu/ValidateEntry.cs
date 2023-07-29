using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValidateEntry : MonoBehaviour {

    [SerializeField] TMP_InputField inputField;
    

    public static int ValidateInput(string inputString) {
        if (inputString != "" && inputString != "-") {
            return int.Parse(inputString);
        } else {
            return 0;
        }
    }

  public static void FlagInvalidEntry() {
      GameManager._instance.warningText.text = "Invalid Entry. Please enter a positive, whole number for all fields.";
     
      GameManager._instance.foodInput.text = "";
      GameManager._instance.energyInput.text = "";
    }

  public static void ClearWarning() {
      GameManager._instance.warningText.text = "";
  }

  
}
