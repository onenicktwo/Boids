using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValidateEntry : MonoBehaviour {

    public static int ValidateInput(string inputString) {
        if (inputString != "" && inputString != "-") {
            return int.Parse(inputString);
        } else {
            return 0;
        }
    }

    public static void FlagInvalidEntry() {
        GameManager._instance.warningText.text = "Invalid Entry. Please enter a positive, whole number for all fields.";
        GameObject.Find("InputManager").GetComponent<InputManager>().ClearInputs();
    }

    public static void ClearWarning() {
        GameManager._instance.warningText.text = "";
    }

  
}
