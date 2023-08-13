using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ValidateEntry : MonoBehaviour {

    public static float ValidateInput(string inputString) {
        try {
            float input = float.Parse(inputString);
            if (input <= 0)
            {
                throw new Exception("Negative input value");
            }
            return input;
        } catch (Exception e) {
            throw e;
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
