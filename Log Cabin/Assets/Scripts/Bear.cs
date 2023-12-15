using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bear : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject player;
    public Animator animationController;

    public GameObject cave;
    public GameObject fireUI;

    bool debugWalkDirection = false; 

    public List<UnityEngine.Collision> overlappingColliders = new List<UnityEngine.Collision>();

    public AudioSource bearAudioSource;

    public AudioClip bearGrowl;
    public GameObject footstep;
    
    void Start(){
        bearAudioSource.clip = (bearGrowl);
        bearAudioSource.Play();
        footstep.SetActive(true);
    }

    void Update()
    {

        var fireSize = fireUI.GetComponent<FireScript>().heat;
        Vector3 adjustedCavePosition = cave.transform.position;

        adjustedCavePosition[0] -= 3;
        adjustedCavePosition[1] += 2;
        adjustedCavePosition[2] += 13;

        Vector3 dirToCave = adjustedCavePosition - transform.position;

        // Calculate movement direction
        Vector3 dirToPlayer = player.transform.position - transform.position;

        // Move the NPC
        if (fireSize > 3){
            MoveNPC(dirToCave);
            //Debug.Log("Move toward " + dirToCave);
        }
        else {
            MoveNPC(dirToPlayer);
            //Debug.Log("Move toward " + dirToPlayer);
        }
    }


    void MoveNPC(Vector3 movementDirection)
    {
        //Debug.Log(movementDirection);

        //If bear has reached it's target
        if (overlappingColliders.Count > 0){
            
            //If its the cave, just stop moving for now
            if (overlappingColliders[0].gameObject.tag == "Cave"){
                if (movementDirection[0] < 3 && movementDirection[2] < 3){
                    animationController.SetBool("is_walking", false);
                    return;
                }
            }
            

        }

        // Calculate the rotation to face the player
        Quaternion lookRot = Quaternion.LookRotation(movementDirection);
        lookRot.x = 0;
        lookRot.z = 0;

        // Smoothly rotate towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Mathf.Clamp01(3.0f * Time.deltaTime));

        // Calculate the position to move towards the player
        Vector3 moveTowards = transform.position + movementDirection.normalized * moveSpeed * Time.deltaTime;

        // Move towards the player
        transform.position = moveTowards;

        // Set walking animation
        animationController.SetBool("is_walking", true);
    }

    private void OnCollisionEnter(Collision other){
        Debug.Log(other.gameObject.tag);

        //If the bears touches the player, they lose
        if (other.gameObject.tag == "Player"){
            SceneManager.LoadScene("YouLoseScreen");
        }
    }

    private void OnCollisionExit(Collision other){
        //Debug.Log("EXITING");
        overlappingColliders.Remove(other);
    }
}