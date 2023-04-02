using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controleSom : MonoBehaviour {

    public AudioSource audioMusic, audioFX;
    public AudioClip somAcerto, somErro;

     private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start() {
  
    }

    // Update is called once per frame
    void Update() {

    }

    public void playAcerto() {
        audioFX.PlayOneShot(somAcerto);
    }
    public void playErro() {
        audioFX.PlayOneShot(somErro);
    }

}
