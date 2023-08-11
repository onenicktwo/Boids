using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {

    public AudioSource gameMusic;
    public AudioSource pauseMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPause() {
        gameMusic.Stop();
        pauseMusic.Play();
    }

    public void EndPause() {
        pauseMusic.Stop();
        gameMusic.Play();
    }


}
