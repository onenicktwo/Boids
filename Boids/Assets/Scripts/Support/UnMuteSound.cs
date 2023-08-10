using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnMuteSound: MonoBehaviour {
    public GameObject xImage;

    public void UnmuteSounds() {
        AudioListener.volume = 1;
        xImage.SetActive(false);
    }
}
