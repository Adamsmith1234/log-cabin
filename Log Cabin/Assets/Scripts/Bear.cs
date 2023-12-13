using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject player;
    public Animator animationController;

    public GameObject cave;
    public GameObject fireUI;

    bool debugWalkDirection = false; 

    void Update()
    {

        var fireSize = fireUI.GetComponent<FireScript>().heat;
        Vector3 adjustedCavePosition = cave.transform.position;

        adjustedCavePosition[0] -= 1;
        adjustedCavePosition[1] += 2;
        adjustedCavePosition[2] += 11;

        Vector3 dirToCave = adjustedCavePosition - transform.position;

        // Calculate movement direction
        Vector3 dirToPlayer = player.transform.position - transform.position;
        
        // Move the NPC
        if (fireSize > 6){
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
}