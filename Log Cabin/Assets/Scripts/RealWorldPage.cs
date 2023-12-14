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
    public BookScript book;

    public AudioSource OtherObjectsAudioSource;

    public AudioClip pickUpPaperClip;
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
        if (isOverlappingPlayer){
            pickUpPageText.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space)){
                    OtherObjectsAudioSource.clip = pickUpPaperClip;
                    OtherObjectsAudioSource.Play();
                levelManager.GetComponent<LEVEL_MANAGER>().levelUp();
                Destroy(gameObject);
                pickUpPageText.enabled = false;
                book.openBook(true);
            }
        }
        else {
            pickUpPageText.enabled = false;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        overlappingCollisions.Add(other);
        if (other.gameObject.tag == "Player") isOverlappingPlayer = true;
    }

    private void OnCollisionExit(Collision other){
        overlappingCollisions.Remove(other);
        if (other.gameObject.tag == "Player") isOverlappingPlayer = false;
    }
}
