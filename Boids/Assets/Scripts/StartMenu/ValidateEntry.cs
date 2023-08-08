using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ValidateEntry : MonoBehaviour {

    public static float ValidateInput(string inputString) {
        if (inputString != "" && inputString != "-") {
            return float.Parse(inputString);
        } else {
            throw new Exception(inputString);
        }
    }

    public static void FlagInvalidEntry() {
        GameManager._instance.warningText.text = "Invalid Entry. Please enter a positive, whole number for all fields.";
        GameObject.Find("InputManager").GetComponent<InputManager>().ClearAllInputs();
    }

    public static void ClearWarning() {
        GameManager._instance.warningText.text = "";
    }

  
}
