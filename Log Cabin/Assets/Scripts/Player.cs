using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;

public class Player : MonoBehaviour
{

    public Animator animation_controller;

    public float speed = 5f; 
    public float turnSpeed = 5f;

    public int woodCount = 0;

    public TextMeshProUGUI woodCountText;

    public TextMeshProUGUI pickUpText;

    public TextMeshProUGUI centerOfScreenPlaceholder;

    public TextMeshProUGUI normalBookPositionPlaceholder;

    public GameObject book;

    public Camera viewportCamera;

    public bool isBookLarge = false;

    public UnityEngine.Vector3 normalBookScale;

    

    public List<UnityEngine.Collider> overlappingColliders = new List<UnityEngine.Collider>();

    // Start is called before the first frame update
    void Start(){
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        pickUpText.enabled = false;
        isBookLarge = false;
        normalBookScale = book.transform.localScale;
    }

    // Update is called once per frame
    void Update(){

        // Get input from the player
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float turn = horizontalInput * turnSpeed * Time.deltaTime;

        MovePlayerHandler();
        pickUpWoodHandler();

        woodCountText.text = "Wood Count: " + woodCount.ToString() +  " Logs";

        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.MovePosition(transform.forward * speed * Time.deltaTime * verticalInput);
        rb.MovePosition(transform.forward * Time.deltaTime * speed * verticalInput + transform.position);
        transform.Rotate(0, turn, 0);

    }

    void MovePlayerHandler(){

        if (Input.GetKey ("up")){
            //Movement input
            animation_controller.SetBool("is_walking", true);
            animation_controller.SetBool("is_walking_backward", false);
            //Debug.Log("walking");
        }

        else {
            animation_controller.SetBool("is_walking", false);
        }

        if (Input.GetKey ("left")){
            //Movement input
            animation_controller.SetBool("is_turning_left", true);
            animation_controller.SetBool("is_turning_right", false);
            animation_controller.SetBool("is_walking", false);
            //Debug.Log("turning left");
        }

        else {
            animation_controller.SetBool("is_turning_left", false);          
        }

        if (Input.GetKey ("right")){
            //Movement input
            animation_controller.SetBool("is_turning_right", true);
            animation_controller.SetBool("is_turning_left", false);
            animation_controller.SetBool("is_walking", false);
            //Debug.Log("turning right");
        }

        else {
            animation_controller.SetBool("is_turning_right", false);
        }


        if (Input.GetKey ("down")){
            //Movement input
            animation_controller.SetBool("is_turning_left", false);
            animation_controller.SetBool("is_turning_right", true);
            animation_controller.SetBool("is_walking", false);
            animation_controller.SetBool("is_walking_backward", true);
            //Debug.Log("turning true");
        }

        if (Input.GetKeyDown("b")){

            if (isBookLarge){
                book.transform.position = normalBookPositionPlaceholder.transform.position;

                book.transform.localScale = normalBookScale;

                isBookLarge = false;
            }

            else {
                book.transform.position = centerOfScreenPlaceholder.transform.position;
                
                book.transform.localScale = new UnityEngine.Vector3 (100, 100, 9);

                isBookLarge = true;
            }


            //int x = (viewportCamera.pixelWidth / 2);
            //int y = (viewportCamera.pixelHeight / 2);

            // Convert screen coordinates to world coordinates
            //Vector3 bookPosition = viewportCamera.ScreenToWorldPoint(new Vector3(x, y, -7));

            // Set the book's position



            Debug.Log("New book position: " + book.transform.position);


            /*if (book.activeSelf == true) {
                book.SetActive(false); 
            }
            else {
                book.SetActive(true); 
            }
            */
        }
        
        else {
            animation_controller.SetBool("is_walking_backward", false);
        }
    }

    //Handles picking up wood based on if there's anything overlapping (for now, just wood)
    void pickUpWoodHandler() {

        //Debug.Log(overlappingColliders.Count);

        if (overlappingColliders.Count > 0){

            pickUpText.enabled = true;

            if (Input.GetKey ("p")){
                animation_controller.SetTrigger("is_picking_up");
                Destroy(overlappingColliders[0].gameObject);
                overlappingColliders.RemoveAt(0);
                woodCount += 1;
                Debug.Log("Wood count: " + woodCount);
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
