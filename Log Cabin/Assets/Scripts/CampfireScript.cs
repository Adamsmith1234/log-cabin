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

    public void updateParticles(float heat, float fireSize) {
        var FlamesMain = mainFlamesSystem.main;
        float scaledHeat = heat/40;
        FlamesMain.startLifetime = Random.Range(.8f,1f)*scaledHeat;
    }
}
