using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;

public class BookScript : MonoBehaviour
{

    public GameObject player;

    public LEVEL_MANAGER levelManager;

    public Animator animationController;

    public TextMeshProUGUI centerOfScreenPlaceholder;

    public TextMeshProUGUI normalBookPositionPlaceholder;

    public bool isBookLarge = false;

    public UnityEngine.Vector3 normalBookScale;

    public GameObject [] pages;
    public int[] pagePerLevel;
    private List<int> pageThresholds;

    public int currentPageNumber = 0;

    private bool isFlipping = false;

    Camera mainCamera;

    public Button leftButton, rightButton;

    public AudioSource OtherObjectsAudioSource;

    public AudioClip turnPageClip, clickOnBookClip;

    // Start is called before the first frame update
    void Start() {
        pageThresholds = new List<int>();
        isBookLarge = false;
        transform.position = normalBookPositionPlaceholder.transform.position;
        normalBookScale = transform.localScale;
        foreach (GameObject page in pages){
            page.SetActive(false);
        }
        pageThresholds.Add(pagePerLevel.Aggregate(0, (acc, x) => {pageThresholds.Add(acc); return acc+x;}));
        while (pageThresholds.Count < levelManager.levelCount) pageThresholds.Add(pageThresholds[pageThresholds.Count-1]);

        mainCamera = Camera.main;

        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);

        pages[0].SetActive(true);
        Debug.Log("Print");
        //Debug.Log(pageThresholds.Count);
        for (int i = 0; i < pageThresholds.Count; i++){
            Debug.Log(pageThresholds[i]);
        }
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Mouse1) && !isBookLarge) {
            openBook();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && isBookLarge && !isFlipping) {
            closeBook();
        }
            /*UnityEngine.Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                // Use the hit variable to determine what was clicked on.
                GameObject clickedObject = hit.collider.gameObject;

                Debug.Log("Clicked on " + clickedObject.tag);

                if (clickedObject.tag == "Book"){
                    if (isBookLarge){
                        transform.position = normalBookPositionPlaceholder.transform.position;
                        transform.localScale = normalBookScale;
                        isBookLarge = false;
                        leftButton.gameObject.SetActive(false);
                        rightButton.gameObject.SetActive(false);
                    }

                    else {
                        transform.position = centerOfScreenPlaceholder.transform.position;      
                        transform.localScale = new UnityEngine.Vector3 (100, 100, 9);
                        isBookLarge = true;
                        leftButton.gameObject.SetActive(true);
                        rightButton.gameObject.SetActive(true);
                    }

                    //change_book_state_handler();
                }
            }*/
    }

    public void openBook(bool pauseTime = false) {
        OtherObjectsAudioSource.clip = clickOnBookClip;
        OtherObjectsAudioSource.Play();
        transform.position = centerOfScreenPlaceholder.transform.position;      
        transform.localScale = new UnityEngine.Vector3 (100, 100, 9);
        isBookLarge = true;
        rightButtonsOn();
        Cursor.visible = true;
        if (pauseTime) Time.timeScale = 0f;
    }

    public void closeBook() {
        OtherObjectsAudioSource.clip = clickOnBookClip;
        OtherObjectsAudioSource.Play();
        transform.position = normalBookPositionPlaceholder.transform.position;
        transform.localScale = normalBookScale;
        isBookLarge = false;
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    //Move to next page
    public void change_book_state_right_button() {
        //If not already flipping and not on final page
        if (!isFlipping && currentPageNumber < pageThresholds[(int) levelManager.currentLevel]){
            //Start animation
            isFlipping = true;
            pages[currentPageNumber].GetComponent<Animator>().SetTrigger("move_forward_a_page");
            OtherObjectsAudioSource.clip = turnPageClip;
            OtherObjectsAudioSource.Play();
            //Turn on next page
            pages[currentPageNumber + 1].SetActive(true);
            //Record new page number
            currentPageNumber += 1;
        }
        rightButtonsOn();
    }
    public void onFlip2() {
        //If need to disable previous page afterwards, do it
        if(currentPageNumber >= 2) pages[currentPageNumber - 2].SetActive(false);
        isFlipping = false;
    }

    //Move to previous page
    public void change_book_state_left_button() {
        //If not already flipping and not on cover
        if (!isFlipping && currentPageNumber > 0){
            //Start animation
            isFlipping = true;
            pages[currentPageNumber-1].GetComponent<Animator>().SetTrigger("move_back_a_page");
            OtherObjectsAudioSource.clip = turnPageClip;
            OtherObjectsAudioSource.Play();
            //Turn on previous page
            if (currentPageNumber > 1) pages[currentPageNumber - 2].SetActive(true);
            //Record new page number
            currentPageNumber -= 1;
        }
        rightButtonsOn();
    }
    public void onUnFlip2() {
        //Turn off next page (that you just flipped from)
        pages[currentPageNumber + 1].SetActive(false);
        isFlipping = false;
    }

    private void rightButtonsOn() {
        Debug.Log(("Current Page:",currentPageNumber));
        rightButton.gameObject.SetActive(currentPageNumber != pageThresholds[(int) levelManager.currentLevel]);
        leftButton.gameObject.SetActive(currentPageNumber != 0);
    }
}
