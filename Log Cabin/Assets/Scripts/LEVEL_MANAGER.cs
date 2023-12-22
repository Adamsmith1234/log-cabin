using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVEL_MANAGER : MonoBehaviour
{

    public int levelCount = 5;

    public GameObject book;
    public enum levels { // ADD NEW VALUES WHEN ADDING NEW LEVELS
        STARTER,
        NEEDS_HEAT,
        NEEDS_FOOD,
        NEEDS_BEAR,
        NEEDS_RESCUE
    }
      // INCREMENT WHEN ADDING NEW LEVELS
    public levels currentLevel = levels.STARTER;
    public GameObject[] LevelUIs;
    public GameObject campfire;

    public GameObject player;

    void Start(){
        if (PlayerPrefs.GetString("Mode") == "Education"){
            LevelUIs[2].SetActive(false);
        }
        else {
            levelUp();
            levelUp();
            levelUp();
            levelUp();
        }
        
    }
    void Update() {
        //if (Input.GetKeyDown(KeyCode.H)) levelUp(); // REPLACE WITH TRIGGER FOR LEVELUP (e.g. picking up page)
        if (currentLevel >= levels.NEEDS_HEAT) LevelUIs[0].GetComponent<FireScript>().updateSystem(Time.deltaTime);
        if (currentLevel >= levels.NEEDS_FOOD) LevelUIs[1].GetComponent<HungerScript>().updateSystem(Time.deltaTime);
    }
    public void levelUp() { // INCREMENTS CURRENT LEVEL WHEN POSSIBLE. TURNS ON THE RELEVANT UI COMPONENT
        if ((int) currentLevel == levelCount-1) {
            Debug.Log("We haven't made the next level yet.");
            return;
        }
        currentLevel += 1;

        // REPLACE WITH MORE COMPREHENSIVE METHOD THAT SPACES OUT THE UI ON SCREEN IF NECESSARY
        LevelUIs[(int) currentLevel - 1].SetActive(true);
        
        if (currentLevel == levels.NEEDS_HEAT) player.GetComponent<Player>().updateWoodUI();
        if (currentLevel == levels.NEEDS_RESCUE) LevelUIs[3].GetComponent<LeafScript>().leavesOn = true;
    }
}
