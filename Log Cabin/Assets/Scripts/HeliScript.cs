using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeliScript : MonoBehaviour
{
    public Camera cam;
    public void victory() {
        SceneManager.LoadScene(3);
    }
}
