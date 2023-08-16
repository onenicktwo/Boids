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
        GameManager._instance.warningPanel.SetActive(true);
        GameManager._instance.blockingPanel.SetActive(true);

    }

    public static void ClearWarning() {
        GameObject.Find("InputManager").GetComponent<InputManager>().ClearAllInputs();
        GameManager._instance.warningPanel.SetActive(false);
        GameManager._instance.blockingPanel.SetActive(false);
    }

  
}
