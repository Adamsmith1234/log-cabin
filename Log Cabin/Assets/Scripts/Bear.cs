using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    // Start is called before the first frame update

    public float moveSpeed = 5f; // Adjust the speed as needed

    public GameObject player;

    void Update()
    {
        // Get user input for movement

        // Calculate movement direction
        Vector3 movement = new Vector3(1f, 0f, 0f);

        // Move the NPC
        MoveNPC(movement);
    }

    void MoveNPC(Vector3 movement)
    {
            Vector3 moveTowards = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            //Function I'm using to restrict the height
            //moveTowards.y = Mathf.Clamp(moveTowards.y, 0.0f, );

            transform.position = moveTowards;
    }
}
