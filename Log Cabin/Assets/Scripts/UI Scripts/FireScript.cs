using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    private float[] heatThresholds = new float[] {0f,2f,7f,12f,20f};
    private float[] heatProduction = new float[] {2f,4f,6f,8f,10f};
    private float[] burnSpeeds = new float[] {1.0f,.8f,.5f,.3f,.1f};

    public GameObject player;

    public void updateSystem(float deltaTime) {
        Debug.Log(fireSize);
        if (Input.GetKeyDown(KeyCode.Y) && player.GetComponent<Player>().woodInventory[(int) fuels.TINDER] > 0) addFuel(fuels.TINDER);
        if (Input.GetKeyDown(KeyCode.U) && player.GetComponent<Player>().woodInventory[(int) fuels.KINDLING] > 0) addFuel(fuels.KINDLING);
        if (Input.GetKeyDown(KeyCode.I) && player.GetComponent<Player>().woodInventory[(int) fuels.SMALL_STICKS] > 0) addFuel(fuels.SMALL_STICKS);
        if (Input.GetKeyDown(KeyCode.O) && player.GetComponent<Player>().woodInventory[(int) fuels.LARGE_STICKS] > 0) addFuel(fuels.LARGE_STICKS);
        if (Input.GetKeyDown(KeyCode.P) && player.GetComponent<Player>().woodInventory[(int) fuels.LOGS] > 0) addFuel(fuels.LOGS);
        for (int x=0; x < 5; x++) {
            if (heat >= heatThresholds[x]) {
                float fuelDelta = Mathf.Clamp(burnSpeeds[x] * deltaTime,0,fuelAmounts[x]);
                float burnMultiplier = 1f;
                if (heat > heatThresholds[x]*3) burnMultiplier = 15f;
                fuelAmounts[x] -= Mathf.Clamp(fuelDelta * burnMultiplier,0,fuelAmounts[x]);
                heat += heatProduction[x]*fuelDelta;
            }
        }
        heat -= heatLoss * deltaTime;
        //Debug.Log("Fire size: "+fireSize.ToString("0.00")+" Heat: "+heat.ToString("0.00"));
        heatBar.percentFilled = heat/(firePitCapacity*2);
        campfire.GetComponent<CampfireScript>().updateParticles(heat,fireSize);

        if (fireSize >= firePitCapacity - fuelSizes[0] && heat == 0f){
            fuelAmounts = new float[] {0f,0f,0f,0f,0f};
        }
    }

    public void addFuel(fuels fuelType) {
        if (fireSize + fuelSizes[(int) fuelType] <= firePitCapacity){
            //Debug.Log("WORKED");
            fuelAmounts[(int) fuelType] += 1;
            player.GetComponent<Player>().woodInventory[(int) fuelType] -= 1;
        } 
    }
}
