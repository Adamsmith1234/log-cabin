using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class Player : MonoBehaviour
{

    public Animator animation_controller;

    public Animator book_animation_controller;

    public float speed = 5f; 
    public float turnSpeed = 5f;

    public int woodCount = 0;

    public TextMeshProUGUI woodCountText;

    public TextMeshProUGUI pickUpText;
    
    public GameObject WoodSpawner;
    public List<UnityEngine.Collider> overlappingColliders = new List<UnityEngine.Collider>();

    public int[] woodInventory;
    public int tinderSlot, kindlingSlot, smallStickSlot, largeStickSlot, logSlot;

    public GameObject hungerUI;

    // Start is called before the first frame update
    void Start(){
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        pickUpText.enabled = false;
        tinderSlot = 0;
        kindlingSlot = 1;
        smallStickSlot = 2;
        largeStickSlot = 3;
        logSlot = 4;
        woodInventory = new int[] {100,100,100,100,100};
    }

    // Update is called once per frame
    void Update(){

        // Get input from the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float turn = horizontalInput * turnSpeed * Time.deltaTime;

        MovePlayerHandler();
        pickUpHandler();

        woodCountText.text = "Inventory: T = " + woodInventory[tinderSlot].ToString() + " K = " + woodInventory[kindlingSlot].ToString() + " sS = " + woodInventory[smallStickSlot].ToString() + " lS = " + woodInventory[largeStickSlot].ToString() + " L = " + woodInventory[logSlot].ToString();

        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.MovePosition(transform.forward * speed * Time.deltaTime * verticalInput);
        rb.MovePosition(transform.forward * Time.deltaTime * speed * verticalInput + transform.position);
        transform.Rotate(0, turn, 0);

    }

    void MovePlayerHandler(){

        if (Input.GetKey ("w")){
            //Movement input
            animation_controller.SetBool("is_walking", true);
            animation_controller.SetBool("is_walking_backward", false);
            //Debug.Log("walking");
        }

        else {
            animation_controller.SetBool("is_walking", false);
        }

        if (Input.GetKey ("a")){
            //Movement input
            animation_controller.SetBool("is_turning_left", true);
            animation_controller.SetBool("is_turning_right", false);
            animation_controller.SetBool("is_walking", false);
            //Debug.Log("turning left");
        }

        else {
            animation_controller.SetBool("is_turning_left", false);          
        }

        if (Input.GetKey ("s")){
            //Movement input
            animation_controller.SetBool("is_turning_right", true);
            animation_controller.SetBool("is_turning_left", false);
            animation_controller.SetBool("is_walking", false);
            //Debug.Log("turning right");
        }

        else {
            animation_controller.SetBool("is_turning_right", false);
        }


        if (Input.GetKey ("d")){
            //Movement input
            animation_controller.SetBool("is_turning_left", false);
            animation_controller.SetBool("is_turning_right", true);
            animation_controller.SetBool("is_walking", false);
            animation_controller.SetBool("is_walking_backward", true);
            //Debug.Log("turning true");
        }

        
        else {
            animation_controller.SetBool("is_walking_backward", false);
        }
    }

    //Handles picking up wood based on if there's anything overlapping (for now, just wood)
    void pickUpHandler() {

        //Debug.Log(overlappingColliders.Count);

        if (overlappingColliders.Count > 0){

            pickUpText.enabled = true;

            if (Input.GetKey ("p")){
                animation_controller.SetTrigger("is_picking_up");
                Debug.Log("Picked up one: " + overlappingColliders[0].gameObject.tag);
                WoodSpawner.GetComponent<WoodSpawnHandler>().restorePoint(overlappingColliders[0].gameObject.GetComponent<WoodPickup>());
                if (overlappingColliders[0].gameObject.tag == "Tinder"){
                    woodInventory[tinderSlot] += 1;
                }
                if (overlappingColliders[0].gameObject.tag == "Kindling"){
                    woodInventory[kindlingSlot] += 1;
                }
                if (overlappingColliders[0].gameObject.tag == "Small Stick"){
                    woodInventory[smallStickSlot] += 1;
                }
                if (overlappingColliders[0].gameObject.tag == "Large Stick"){
                    woodInventory[largeStickSlot] += 1;
                }
                if (overlappingColliders[0].gameObject.tag == "Log"){
                    woodInventory[logSlot] += 1;
                }
                if (overlappingColliders[0].gameObject.tag == "Blueberry"){
                    hungerUI.GetComponent<HungerScript>().eatBlueberry();;
                }

                Destroy(overlappingColliders[0].gameObject);
                overlappingColliders.RemoveAt(0);
                animation_controller.SetTrigger("done_picking_up");
            }
        }
        else {
            pickUpText.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ENTERING");
        overlappingColliders.Add(other);
    }

    private void OnTriggerExit(Collider other){
        //Debug.Log("EXITING");
        overlappingColliders.Remove(other);
    }

}
