using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound: MonoBehaviour {
    public GameObject xImage;

    public void MuteSounds() {
        AudioListener.volume = 0;
        xImage.SetActive(true);
    }
}
