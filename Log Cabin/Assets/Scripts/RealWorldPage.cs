using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RealWorldPage : MonoBehaviour
{

    public List<UnityEngine.Collision> overlappingCollisions = new List<UnityEngine.Collision>();

    public GameObject levelManager;

    public TextMeshProUGUI pickUpPageText;

    bool isOverlappingPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        pickUpPageText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        //Debug.Log(transform.position);
        //To account for the ground
        if (overlappingCollisions.Count > 1){

            foreach (Collision collision in overlappingCollisions){
                if (collision.gameObject.tag == "Player"){
                    isOverlappingPlayer = true;
                }
                else {
                    isOverlappingPlayer = false;
                }
            }


            if (isOverlappingPlayer){
                pickUpPageText.enabled = true;
                if (Input.GetKeyDown("space")){
                    levelManager.GetComponent<LEVEL_MANAGER>().levelUp();
                    Destroy(gameObject);
                    pickUpPageText.enabled = false;
                }
            }

        }
        else {
            pickUpPageText.enabled = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("ENTERING");
        overlappingCollisions.Add(other);
        Debug.Log(other.gameObject.tag);
    }

    private void OnCollisionExit(Collision other){
        //Debug.Log("EXITING");
        overlappingCollisions.Remove(other);
        Debug.Log("PLAYER OFF PAGE");
    }
}
