using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ReproSlider : MonoBehaviour
{
    [SerializeField] public Slider _reproSlider;
    [SerializeField] public TextMeshProUGUI _reproSliderTxt;
    
    [SerializeField] public Slider _hungrySlider;
    [SerializeField] public TextMeshProUGUI _hungrySliderTxt;



    // Start is called before the first frame update
    void Start() {
        _hungrySlider.onValueChanged.AddListener((vh) => {
                
            if(vh < _reproSlider.value - 0.05) {
                _hungrySliderTxt.text = vh.ToString("0%");
            } else {
                _hungrySlider.value = _reproSlider.value - 0.05f;
            }
                        
        });

        _reproSlider.onValueChanged.AddListener((vr) => {
            if(vr > _hungrySlider.value + 0.05) {
                _reproSliderTxt.text = vr.ToString("0%");
            } else {
                _reproSlider.value = _hungrySlider.value + 0.05f;
            }
            
            

        });
    }

  
}
