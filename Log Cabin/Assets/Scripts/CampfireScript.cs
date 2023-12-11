using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CampfireScript : MonoBehaviour
{

    public ParticleSystem mainFlamesSystem;
    public ParticleSystem smokeSystem;
    public ParticleSystem redFlamesSystem;

    public GameObject player;

    public bool flamesOn;

    private bool hasPlacedWood = false;

    public Component[] particleSystems;

    public TextMeshProUGUI fireSizeText;


    // Start is called before the first frame update
    void Start()
    {
        var mainFlamesEmission = mainFlamesSystem.emission; // Stores the module in a local variable
        var smokeEmission = smokeSystem.emission; // Stores the module in a local variable
        var redFlamesEmission = redFlamesSystem.emission; // Stores the module in a local variable

        mainFlamesEmission.enabled = true; // Applies the new value directly to the Particle System
        smokeEmission.enabled = true; // Applies the new value directly to the Particle System
        redFlamesEmission.enabled = true; // Applies the new value directly to the Particle System

        flamesOn = true;

        particleSystems = GetComponentsInChildren<ParticleSystem>(); //Child particle systems
        mainFlamesSystem.Stop();
        smokeSystem.Stop();
        redFlamesSystem.Stop();
    }

    public void turnOnFire() {
        mainFlamesSystem.Play();
        smokeSystem.Play();
        redFlamesSystem.Play();
    }
    public void turnOffFire() {
        mainFlamesSystem.Stop();
        smokeSystem.Stop();
        redFlamesSystem.Stop();
    }

    public void updateParticles(float heat, float fireSize) {
        var FlamesMain = mainFlamesSystem.main;
        var redFlamesMain = redFlamesSystem.main;
        var smokeMain = smokeSystem.main;
        float scaledHeat = Mathf.Log(heat+1,41);
        FlamesMain.startLifetime = flameHeight(scaledHeat,fireSize);
        redFlamesMain.startLifetime = redFlameHeight(scaledHeat,fireSize);
        smokeMain.startLifetime = smokeHeight(scaledHeat,fireSize);
    }

    float flameHeight(float scaledHeat, float fireSize) {
        return Random.Range(scaledHeat-.2f,scaledHeat+.2f);
    }
    float redFlameHeight(float scaledHeat, float fireSize) {
        return Random.Range(scaledHeat-.7f,scaledHeat-.3f);
    }
    float smokeHeight(float scaledHeat, float fireSize) {
        float inverseScaledHeat = 1/scaledHeat;
        return Random.Range(inverseScaledHeat-.2f,inverseScaledHeat+.2f);
    }
}
