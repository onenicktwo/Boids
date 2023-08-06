using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FoodAmt : MonoBehaviour {
    
    [SerializeField] TextMeshProUGUI foodAmtText;


    public void Start() {
        foodAmtText.text = GameManager._instance.foodCount.ToString();
    }

    public void Update() {
       
        foodAmtText.text = GameManager._instance.foodCount.ToString();
    }


}
