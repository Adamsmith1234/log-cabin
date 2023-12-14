using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using Unity.VisualScripting;
using System.Linq;

public class BookScript : MonoBehaviour
{

    public GameObject player;

    public GameObject levelManager;

    public Animator animationController;

    public TextMeshProUGUI centerOfScreenPlaceholder;

    public TextMeshProUGUI normalBookPositionPlaceholder;

    public bool isBookLarge = false;

    public UnityEngine.Vector3 normalBookScale;

    public GameObject [] pages;

    public int currentPageNumber = 0;

    public List<GameObject> flipped = new List<GameObject>();

    public List<GameObject> notFlipped = new List<GameObject>();

    private bool isFlipping = false;

    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        isBookLarge = false;
        transform.position = normalBookPositionPlaceholder.transform.position;
        normalBookScale = transform.localScale;
        //page1.SetActive(false);
        foreach (GameObject page in pages){
            page.SetActive(false);
            notFlipped.Add(page);
        }

        mainCamera = Camera.main;

        pages[0].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {

       if (Input.GetMouseButtonDown(0))
       {
           UnityEngine.Vector3 mousePosition = Input.mousePosition;
           Ray ray = mainCamera.ScreenPointToRay(mousePosition);
           if (Physics.Raycast(ray, out RaycastHit hit))
           {
               // Use the hit variable to determine what was clicked on.
               GameObject clickedObject = hit.collider.gameObject;

               Debug.Log("Clicked on " + clickedObject.tag);

               if (clickedObject.tag == "Book"){
                    if (isBookLarge){
                        transform.position = normalBookPositionPlaceholder.transform.position;
                        transform.localScale = normalBookScale;
                        isBookLarge = false;
                    }

                    else {
                        transform.position = centerOfScreenPlaceholder.transform.position;      
                        transform.localScale = new UnityEngine.Vector3 (100, 100, 9);
                        isBookLarge = true;
                    }

                    change_book_state_handler();
                }
            }
       }
        
    }

    public void onFlip2(){
        if(currentPageNumber >= 2) pages[currentPageNumber - 2].SetActive(false);
        isFlipping = false;
    }

    public void onUnFlip2(){
        pages[currentPageNumber + 1].SetActive(false);
        isFlipping = false;
    }

    public void change_book_state_handler(){
        int currentLevelCap = (int) levelManager.GetComponent<LEVEL_MANAGER>().currentLevel;

        void onFlip1(){
            GameObject currentPage = notFlipped.ElementAt(0);
            notFlipped.RemoveAt(0);

            flipped.Insert(0, currentPage);

            currentPage.GetComponent<Animator>().SetTrigger("move_forward_a_page");

            pages[currentPageNumber + 1].SetActive(true);

            currentPageNumber += 1;

        }

        void onUnFlip1(){

            GameObject currentPage = flipped.ElementAt(0);
            flipped.RemoveAt(0);

            notFlipped.Insert(0, currentPage);

            currentPage.GetComponent<Animator>().SetTrigger("move_back_a_page");

            if (currentPageNumber >= 2){
                pages[currentPageNumber - 2].SetActive(true);
            }

            currentPageNumber -= 1;
        }

        if (isBookLarge && !isFlipping) {
            if (currentPageNumber < currentLevelCap){
                if (Input.GetKeyDown("k")){
                    isFlipping = true;
                    onFlip1();

                }
            }

            if (currentPageNumber > 0){
                if (Input.GetKeyDown("j")){
                    isFlipping = true;
                    onUnFlip1();

                } 
            }
        }
    }
}
