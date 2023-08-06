using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderValueGetter : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    public bool isPercentage = false;

    private void Update()
    {
        if (isPercentage)
            gameObject.GetComponent<TextMeshProUGUI>().text = _slider.value.ToString("0%");
        else
            gameObject.GetComponent<TextMeshProUGUI>().text = _slider.value.ToString("F2");
    }
}
