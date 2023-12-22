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

    void Start() {
        Cursor.lockState = CursorLockMode.Confined;
        if (!PlayerPrefs.HasKey("Sensitivity-X"))
        {
            PlayerPrefs.SetFloat("Sensitivity-X", -10);
        }
        if (!PlayerPrefs.HasKey("Sensitivity-Y"))
        {
            PlayerPrefs.SetFloat("Sensitivity-Y", -50);
        }

    }
    public void turnOnExposition() {
        startMenu.SetActive(false);
        exposition.SetActive(true);
        bear.SetActive(false);
        player.SetActive(false);
        campfire.SetActive(false);
        ground.SetActive(false);
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
        PlayerPrefs.SetString("Mode", "Education");
        Debug.Log(PlayerPrefs.GetString("Mode"));
    }
    public void goToGame() {
        ButtonClickAudioSource.clip = buttonClick;
        ButtonClickAudioSource.Play();
        SceneManager.LoadScene("CurrentMain");
    }

    public void goToHardcore(){
        PlayerPrefs.SetString("Mode", "Hardcore");
        SceneManager.LoadScene("HardcoreScene");
        Debug.Log(PlayerPrefs.GetString("Mode"));
    }
}
