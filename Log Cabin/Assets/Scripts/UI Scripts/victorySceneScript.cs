using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class victorySceneScript : MonoBehaviour
{


    public AudioSource ButtonClickAudioSource;

    public AudioClip buttonClick;

    private void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void goToGame() {
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
        SceneManager.LoadScene("StartMenu");
    }
}
