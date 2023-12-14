using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Animator animation_controller;

    public Animator book_animation_controller;

    public float speed = 5f; 
    public float turnSpeed = 500f;

    public int woodCount = 0;

    public TextMeshProUGUI pickUpText;
    
    public GameObject WoodSpawner;
    public List<UnityEngine.Collider> overlappingColliders = new List<UnityEngine.Collider>();

    public int[] woodInventory;
    public GameObject fireUI;

    public GameObject hungerUI;

    // Start is called before the first frame update
    void Start(){
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        pickUpText.enabled = false;
        woodInventory = new int[] {0,0,0,0,0};
    }

    // Update is called once per frame
    void Update(){

        // Get input from the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        //float turn = horizontalInput * turnSpeed * Time.deltaTime;

        MovePlayerHandler();
        pickUpHandler();

        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.MovePosition(transform.forward * speed * Time.deltaTime * verticalInput);
        rb.MovePosition(transform.forward * Time.deltaTime * speed * verticalInput + transform.position);
        //transform.Rotate(0, turn, 0);

        float mouseX = turnSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;

        // Rotate the player based on the mouse input
        transform.Rotate(0, mouseX, 0);

    

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

                Debug.Log("Picked up one: " + overlappingColliders[0].gameObject.tag);
                if (overlappingColliders[0].gameObject.tag == "Blueberry"){
                    if (Input.GetKeyDown(KeyCode.Space)){
                        animation_controller.SetTrigger("is_picking_up");
                        animation_controller.SetTrigger("done_picking_up");
                        hungerUI.GetComponent<HungerScript>().eatBlueberry();
                    }
                }
                else if (overlappingColliders[0].gameObject.tag == "Baneberry"){
                    if (Input.GetKeyDown(KeyCode.Space)){
                        animation_controller.SetTrigger("is_picking_up");
                        animation_controller.SetTrigger("done_picking_up");
                        hungerUI.GetComponent<HungerScript>().eatBaneberry();
                    }
                } else {
                    WoodSpawner.GetComponent<WoodSpawnHandler>().restorePoint(overlappingColliders[0].gameObject.GetComponent<WoodPickup>());
                    woodInventory[(int) overlappingColliders[0].gameObject.GetComponent<WoodPickup>().type] += 1;
                    fireUI.GetComponent<FireScript>().pickUpFuel(overlappingColliders[0].gameObject.GetComponent<WoodPickup>().type);
                }


                Destroy(overlappingColliders[0].gameObject);
                overlappingColliders.RemoveAt(0);
            
        }
        else {
            pickUpText.enabled = false;
        }
    }

    public void updateWoodUI() {
        for (int x = 0; x < 5; x++) {
            fireUI.GetComponent<FireScript>().woodInventoryUI[x].GetComponent<Text>().text = woodInventory[x].ToString();
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
