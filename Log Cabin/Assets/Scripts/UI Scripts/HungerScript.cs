using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerScript : MonoBehaviour
{
    public BarGauge stomach;

    void Start() {
        stomach.percentFilled = 1f;
    }
    public void updateSystem(float deltaTime) {
        stomach.percentFilled -= deltaTime/50;
    }

    public void eatBlueberry(){
        stomach.percentFilled += .1f;
        Debug.Log(stomach.percentFilled);
    }
}
