using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEVEL_MANAGER : MonoBehaviour
{

    private int levelCount = 4;

    public GameObject book;
    public enum levels { // ADD NEW VALUES WHEN ADDING NEW LEVELS
        STARTER,
        NEEDS_HEAT,
        NEEDS_FOOD,
        NEEDS_BEAR
    }
      // INCREMENT WHEN ADDING NEW LEVELS
    public levels currentLevel = levels.STARTER;
    public GameObject[] LevelUIs;
    public GameObject campfire;

    public GameObject bear;

    void Start(){
        bear.SetActive(false);
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) levelUp(); // REPLACE WITH TRIGGER FOR LEVELUP (e.g. picking up page)
        if (currentLevel >= levels.NEEDS_HEAT) LevelUIs[0].GetComponent<FireScript>().updateSystem(Time.deltaTime);
        if (currentLevel >= levels.NEEDS_FOOD) LevelUIs[1].GetComponent<HungerScript>().updateSystem(Time.deltaTime);
        if (currentLevel >= levels.NEEDS_BEAR) bear.SetActive(true);
    }
    public void levelUp() { // INCREMENTS CURRENT LEVEL WHEN POSSIBLE. TURNS ON THE RELEVANT UI COMPONENT
        if ((int) currentLevel == levelCount-1) {
            Debug.Log("We haven't made the next level yet.");
            return;
        }
        currentLevel += 1;

        GameObject [] bookPages = book.GetComponent<BookScript>().pages;

        Debug.Log((int) currentLevel);

        if (bookPages.Length >= (int) currentLevel){
            //bookPages[(int) currentLevel].SetActive(true);
        }

        // REPLACE WITH MORE COMPREHENSIVE METHOD THAT SPACES OUT THE UI ON SCREEN IF NECESSARY
        if (((int) currentLevel -1) <2){
            LevelUIs[(int) currentLevel - 1].SetActive(true);
        }
    }
}
