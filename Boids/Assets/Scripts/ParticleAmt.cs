using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleAmt : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI particleAmtText;


    public void Start() {
        particleAmtText.text = GameManager._instance.foodInput.text;
    }

    public void Update() {
        particleAmtText.text = GameManager._instance.foodCount.ToString();
    }
}
