using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animation_controller;

    public float speed = 5f; 
    public float turnSpeed = 5f;

    // Start is called before the first frame update
    void Start(){
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update(){

        // Get input from the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float turn = horizontalInput * turnSpeed * Time.deltaTime;


        if (Input.GetKey ("up")){
            //Movement input
            animation_controller.SetBool("is_walking", true);
            Debug.Log("walking");
        }

        else {
            animation_controller.SetBool("is_walking", false);
        }

        if (Input.GetKey ("left")){
            //Movement input
            animation_controller.SetBool("is_turning_left", true);
            animation_controller.SetBool("is_turning_right", false);
            animation_controller.SetBool("is_walking", false);
            Debug.Log("turning left");
        }

        else {
            animation_controller.SetBool("is_turning_left", false);          
        }

        if (Input.GetKey ("right")){
            //Movement input
            animation_controller.SetBool("is_turning_left", false);
            animation_controller.SetBool("is_turning_right", true);
            animation_controller.SetBool("is_walking", false);
            Debug.Log("turning true");
        }
        
        else {
            animation_controller.SetBool("is_turning_right", false);
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.MovePosition(transform.forward * speed * Time.deltaTime * verticalInput);
        rb.MovePosition(transform.forward * Time.deltaTime * speed * verticalInput + transform.position);
        transform.Rotate(0, turn, 0);
    }

    void MovePlayer(Vector3 direction){

            // Get the Rigidbody component attached to the GameObject
            Rigidbody rb = GetComponent<Rigidbody>();

            Vector3 newPosition = rb.position + (direction * speed * Time.deltaTime);

            //newPosition.y = Mathf.Clamp(newPosition.y, 0f, 7.6f);

            // Apply movement to the Rigidbody
            rb.MovePosition(newPosition);
    }

    /*void RotatePlayer(float turn){

    // Get the Rigidbody component attached to the GameObject
        Rigidbody rb = GetComponent<Rigidbody>();

        // Calculate rotation
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply rotation to the Rigidbody
        rb.MoveRotation(rb.rotation * turnRotation);
        }
        */
}
