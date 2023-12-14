using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject exposition;
    public void turnOnExposition() {
        startMenu.SetActive(false);
        exposition.SetActive(true);
    }
    public void goToGame() {
        SceneManager.LoadScene(0);
    }
}
