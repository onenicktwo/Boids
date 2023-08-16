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
        InputManager inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        inputManager.warningPanel.SetActive(true);
        inputManager.blockingPanel.SetActive(true);

    }
}
