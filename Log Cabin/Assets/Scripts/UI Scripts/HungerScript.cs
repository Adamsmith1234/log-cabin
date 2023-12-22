using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HungerScript : MonoBehaviour
{
    public BarGauge stomach;

    void Start() {
        stomach.percentFilled = 1f;
    }

    void Update(){
        if (stomach.percentFilled <= 0){
            die();
        }
    }
    public void updateSystem(float deltaTime) {
        if (PlayerPrefs.GetString("Mode") == "Education"){
            stomach.percentFilled -= deltaTime/130;
        }
        else {
            stomach.percentFilled -= deltaTime/110;
        }
        
    }

    public void eatBlueberry(){
        if (PlayerPrefs.GetString("Mode") == "Education"){
            stomach.percentFilled += .1f;
        }
        else {
            stomach.percentFilled += .05f;
        }

        Debug.Log(stomach.percentFilled);
    }

    public void eatBaneberry(){
        if (PlayerPrefs.GetString("Mode") == "Education") {
            stomach.percentFilled -= .2f;
        }
        else {
            stomach.percentFilled -= .3f;
        }
        Debug.Log(stomach.percentFilled);
    }

    public void die(){
        SceneManager.LoadScene("YouLoseScreen");
    }
}
