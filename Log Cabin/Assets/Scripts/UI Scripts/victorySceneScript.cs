using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victorySceneScript : MonoBehaviour
{


    public AudioSource ButtonClickAudioSource;

    public AudioClip buttonClick;
    public void goToGame() {
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
        SceneManager.LoadScene(0);
    }
}
