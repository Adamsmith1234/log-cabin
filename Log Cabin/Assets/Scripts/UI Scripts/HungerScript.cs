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
        stomach.percentFilled -= deltaTime/300;
    }

    public void eatBlueberry(){
        stomach.percentFilled += .1f;
        Debug.Log(stomach.percentFilled);
    }

    public void eatBaneberry(){
        stomach.percentFilled -= .2f;
        Debug.Log(stomach.percentFilled);
    }

    public void die(){
        SceneManager.LoadScene("YouLoseScreen");
    }
}
