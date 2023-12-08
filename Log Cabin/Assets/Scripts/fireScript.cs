using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fireScript : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {

        var mainFlamesEmission = mainFlamesSystem.emission; // Stores the module in a local variable
        var smokeEmission = smokeSystem.emission; // Stores the module in a local variable
        var redFlamesEmission = redFlamesSystem.emission; // Stores the module in a local variable

        var main = mainFlamesSystem.main; //Only using this one for the text for now, will need to use others eventually

        fireSizeText.text = "Fire Size: " + string.Format("{0:N2}", main.duration) + " Units";

        //Debug.Log(main.duration);

        if (Input.GetKeyDown("space")){
            if (player.GetComponent<Player>().woodCount > 0 && !hasPlacedWood){

                hasPlacedWood = true;

                //This goes through and applies this to each of it's children
                foreach (ParticleSystem childParticleSystem in particleSystems){
                    childParticleSystem.Stop(); 
                    childParticleSystem.Clear();
                    var childMain = childParticleSystem.main;
                    childMain.duration += 1;
                    childMain.startLifetime = childMain.duration;

			        childParticleSystem.Play();
		        }

                player.GetComponent<Animator>().SetTrigger("is_picking_up");
                player.GetComponent<Animator>().SetTrigger("done_picking_up");


                Debug.Log("Added to fire");
                player.GetComponent<Player>().woodCount -= 1;
            }
        }

        else {
            hasPlacedWood = false;
        }
        
    }

    /*void PlaceWoodHandler()
    {
        if (player.GetComponent<Player>().woodCount > 0 && !hasPlacedWood)
        {
            hasPlacedWood = true;

            var main = mainFlamesSystem.main;

            mainFlamesSystem.Stop();
            mainFlamesSystem.Clear();
            main.duration += 1;
            main.startLifetime = main.duration;
            mainFlamesSystem.Play();

            foreach (ParticleSystem childParticleSystem in children)
            {
                childParticleSystem.Stop();
                var childMain = childParticleSystem.main;
                childMain.duration += 1;
                childMain.startLifetime = childMain.duration;
                childParticleSystem.Play();
            }

            player.GetComponent<Animator>().SetTrigger("is_picking_up");
            player.GetComponent<Animator>().SetTrigger("done_picking_up");

            Debug.Log("Added to fire");
            player.GetComponent<Player>().woodCount -= 1;
        }
    }
    */
}
