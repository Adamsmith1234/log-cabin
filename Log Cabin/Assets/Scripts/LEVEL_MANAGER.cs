using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVEL_MANAGER : MonoBehaviour
{
    enum levels { // ADD NEW VALUES WHEN ADDING NEW LEVELS
        STARTER,
        NEEDS_HEAT,
        NEEDS_FOOD,
        NEEDS_WATER
    }
    int levelCount = 3;  // INCREMENT WHEN ADDING NEW LEVELS
    levels currentLevel = levels.STARTER;
    public GameObject[] LevelUIs;
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) levelUp(); // REPLACE WITH TRIGGER FOR LEVELUP (e.g. picking up page)
        if (currentLevel >= levels.NEEDS_HEAT) LevelUIs[0].GetComponent<FireScript>().updateSystem(Time.deltaTime);
        if (currentLevel >= levels.NEEDS_FOOD) LevelUIs[1].GetComponent<HungerScript>().updateSystem(Time.deltaTime);
    }
    void levelUp() { // INCREMENTS CURRENT LEVEL WHEN POSSIBLE. TURNS ON THE RELEVANT UI COMPONENT
        if ((int) currentLevel == levelCount-1) {
            Debug.Log("We haven't made the next level yet.");
            return;
        }
        currentLevel += 1;
        // REPLACE WITH MORE COMPREHENSIVE METHOD THAT SPACES OUT THE UI ON SCREEN IF NECESSARY
        LevelUIs[(int) currentLevel - 1].SetActive(true);
    }
}
