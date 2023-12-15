using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject exposition;

    public AudioSource ButtonClickAudioSource;

    public AudioClip buttonClick;

    public GameObject bear, player, campfire, ground;
    public void turnOnExposition() {
        startMenu.SetActive(false);
        exposition.SetActive(true);
        bear.SetActive(false);
        player.SetActive(false);
        campfire.SetActive(false);
        ground.SetActive(false);
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
    }
    public void goToGame() {
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
        SceneManager.LoadScene(0);
    }
}
