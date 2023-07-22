using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodAmt : MonoBehaviour {
    
    [SerializeField] TextMeshProUGUI foodAmtText;


    public void Start() {
        foodAmtText.text = GameManager._instance.foodInput.text;
    }

    public void Update() {
        foodAmtText.text = GameManager._instance.foodCount.ToString();
    }

}