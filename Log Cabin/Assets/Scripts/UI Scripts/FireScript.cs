using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    public BarGauge heatBar;
    public BarGauge sizeBar;
    
    public enum fuels {
        TINDER,
        KINDLING,
        SMALL_STICKS,
        LARGE_STICKS,
        LOGS
    }
    private float[] fuelAmounts = new float[] {0f,0f,0f,0f,0f};
    public float fireSize {
        get {
            float total = 0;
            for (int x = 0; x < 5; x++) total += fuelAmounts[x] * fuelSizes[x];
            return total;
        }
    }
    public int firePitCapacity = 20;
    public float heat {
        get {return _heat;}
        set {_heat = Mathf.Clamp(value,0f,1000f);}
    }
    public GameObject campfire;
    private float _heat = 0;
    private float heatLoss = .3f;
    private float[] fuelSizes = new float[] {1f,2f,4f,6f,10f};
    private float[] heatThresholds = new float[] {0f,1.10f,3.41f,8.28f,18.51f};
    private float[] heatProduction = new float[] {.5f,1.25f,2f,5f,15f};
    private float[] burnSpeeds = new float[] {3f,1f,.5f,.3f,.1f};
    private bool isLit = false;

    public int matches = 3;
    public GameObject[] matchObjects = new GameObject[3];
    public GameObject[] woodInventoryUI = new GameObject[5];

    public void updateSystem(float deltaTime) {
        if (Input.GetKeyDown(KeyCode.Y)) addFuel(fuels.TINDER);
        if (Input.GetKeyDown(KeyCode.U)) addFuel(fuels.KINDLING);
        if (Input.GetKeyDown(KeyCode.I)) addFuel(fuels.SMALL_STICKS);
        if (Input.GetKeyDown(KeyCode.O)) addFuel(fuels.LARGE_STICKS);
        if (Input.GetKeyDown(KeyCode.P)) addFuel(fuels.LOGS);
        if (Input.GetKeyDown(KeyCode.M) && !isLit && matches > 0) {
            matches -= 1;
            matchObjects[matches].SetActive(false);
            isLit = true;
            campfire.GetComponent<CampfireScript>().turnOnFire();
        }
        if (isLit) {
            for (int x=0; x < 5; x++) {
                if (heat >= heatThresholds[x]) {
                    float fuelDelta = Mathf.Clamp(burnSpeeds[x] * deltaTime,0,fuelAmounts[x]);
                    float burnMultiplier = 1f;
                    if (heat > (heatThresholds[x]+1)*2) burnMultiplier = 15f;
                    fuelAmounts[x] -= Mathf.Clamp(fuelDelta * burnMultiplier,0,fuelAmounts[x]);
                    heat += heatProduction[x]*fuelDelta;
                }
            }
            heat = Mathf.Max(0,heat - heatLoss * deltaTime);
            campfire.GetComponent<CampfireScript>().updateParticles(heat,fireSize);
            if (heat == 0f) {isLit = false;campfire.GetComponent<CampfireScript>().turnOffFire();}
        }
        heatBar.percentFilled = Mathf.Log(heat+1,41);
    }

    public void addFuel(fuels fuelType) {
        if (fireSize + fuelSizes[(int) fuelType] <= firePitCapacity) fuelAmounts[(int) fuelType] += 1;
    }
}
