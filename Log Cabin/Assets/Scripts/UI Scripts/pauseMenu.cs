using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public bool paused = false;
    public GameObject menu;

    public void restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void pause() {
        Time.timeScale = 0f;
        paused = true;
        menu.SetActive(true);
        Cursor.visible = true;
    }

    public void unPause() {
        Time.timeScale = 1f;
        paused = false;
        menu.SetActive(false);
        Cursor.visible = false;
    }
}
