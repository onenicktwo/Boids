using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour {

    public GameObject panel;

    public void CloseWindow() {
        panel.SetActive(false);
    }

    public void OpenWindow() {
        panel.SetActive(true);
    }
}
